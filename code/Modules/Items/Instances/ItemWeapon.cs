using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.ScreenShake;
using ssl.Modules.Selection;
using ssl.Player;
using ssl.Player.Animators;

namespace ssl.Modules.Items.Instances;

public partial class ItemWeapon : Item
{
    private const string MUZZLE_ATTACHMENT_NAME = "muzzle";
    private const float MAX_RANGE = 5000;


    [Net] public float PrimaryRate { get; set; }
    [Net] public float Range { get; set; }
    [Net] public float Damage { get; set; }
    [Net] public string ShootSound { get; set; }
    [Net] public string DryFireSound { get; set; }
    [Net] public string ReloadSound { get; set; }
    [Net] public string MuzzleFlashParticle { get; set; }

    protected virtual float BulletSpread { get; set; } = 0.05F;
    protected virtual float BulletForce { get; set; } = 1.5F;
    protected virtual float BulletSize { get; set; } = 3F;

    /// <summary>
    ///     Maximum amount of ammo in one magazine (or equivalent).
    ///     -1 means that the weapon doesn't have any magazine (melee weapon).
    /// </summary>
    [Net] public int MaxAmmo { get; set; }

    /// <summary>
    ///     Current amount of ammo in the current magazine.
    /// </summary>
    [Net, Predicted] public int CurrentAmmo { get; set; }

    /// <summary>
    ///     Time that the weapon will take to reload.
    /// </summary>
    [Net] public float ReloadTime { get; set; }

    [Net, Predicted] protected TimeSince TimeSincePrimaryAttack { get; set; }
    [Net, Predicted] protected bool IsReloading { get; set; }
    [Net, Predicted] protected TimeSince TimeSinceStartReload { get; set; }

    protected TraceResult Hit { get; private set; }
    protected Entity HitEntity => Hit.Entity;

    public override void OnDownUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnDownUsePrimary(sslPlayer, target);

        if (CanPrimaryAttack()) AttackPrimary();
    }

    public override void Simulate(Client cl)
    {
        if (Input.Pressed(InputButton.Reload))
        {
            IsReloading = true;
            TimeSinceStartReload = 0F;

            using (Prediction.Off())
            {
                ReloadEffects();
            }
        }
        else if (CanReload())
        {
            Reload();
        }
    }

    protected bool CanReload()
    {
        return Owner.IsValid() && IsReloading && TimeSinceStartReload >= ReloadTime;
    }

    protected void Reload()
    {
        CurrentAmmo = MaxAmmo;
        IsReloading = false;
    }

    private bool CanPrimaryAttack()
    {
        if (!Owner.IsValid())
            return false;

        if (PrimaryRate <= 0)
            return true;

        return TimeSincePrimaryAttack > 1 / PrimaryRate;
    }

    protected virtual void AttackPrimary()
    {
        TimeSincePrimaryAttack = 0;

        //TODO SSL-382: Bullet to class

        if (CurrentAmmo == 0 && MaxAmmo > 0)
        {
            using (Prediction.Off())
            {
                DryFireEffects();
            }
        }
        else
        {
            ShootBullet(BulletSpread, BulletForce, BulletSize);
            ConsumeAmmo();

            using (Prediction.Off())
            {
                FireEffects();
            }
        }
    }

    /// <summary>
    ///     Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple
    ///     hits, like if you're going through layers or ricocet'ing or something.
    /// </summary>
    protected virtual TraceResult TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f)
    {
        bool inWater = Map.Physics.IsPointWater(start);

        TraceResult tr = Trace.Ray(start, end)
            .UseHitboxes()
            .HitLayer(CollisionLayer.Water, !inWater)
            .Ignore(Owner)
            .Ignore(this)
            .Size(radius)
            .Run();

        return tr;

        //
        // Another trace, bullet going through thin material, penetrating water surface?
        //
    }

    public override void SimulateAnimator(HumanAnimator animator)
    {
        base.SimulateAnimator(animator);
        animator.SetAnimParameter(HANDEDNESS_KEY, 0);
    }

    protected virtual void ShootBullet(float spread, float force, float bulletSize)
    {
        Vector3 bulletDirection = Owner.EyeRotation.Forward;
        bulletDirection += Vector3.Random * spread;
        bulletDirection = bulletDirection.Normal;

        //If the range is 0, the range is max.
        float range = Range == 0 ? MAX_RANGE : Range;
        TraceResult tr = TraceBullet(Owner.EyePosition, Owner.EyePosition + bulletDirection * range, bulletSize);

        Hit = tr;

        if (!IsServer || !tr.Entity.IsValid()) return;

        tr.Surface.DoBulletImpact(tr);

        DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPosition, bulletDirection * force, Damage)
            .UsingTraceResult(tr)
            .WithAttacker(Owner)
            .WithWeapon(this);

        tr.Entity.TakeDamage(damageInfo);
    }

    protected void ConsumeAmmo()
    {
        if (CurrentAmmo > 0) CurrentAmmo--;
    }

    [ClientRpc]
    protected virtual void FireEffects()
    {
        Entity effectEntity = GetEffectEntity();

        if (IsLocalPawn) _ = new Perlin();

        Sound.FromEntity(ShootSound, effectEntity);
        Particles.Create(MuzzleFlashParticle, effectEntity, MUZZLE_ATTACHMENT_NAME);
    }

    [ClientRpc]
    protected virtual void DryFireEffects()
    {
        Entity effectEntity = GetEffectEntity();
        Sound.FromEntity(DryFireSound, effectEntity);
    }

    [ClientRpc]
    protected virtual void ReloadEffects()
    {
        Entity effectEntity = GetEffectEntity();
        Sound.FromEntity(ReloadSound, effectEntity);
    }

    protected virtual Entity GetEffectEntity()
    {
        Host.AssertClient();

        Entity effectEntity;

        if (IsLocalPawn && Local.Pawn is SslPlayer player)
            effectEntity = player.Inventory.ViewModel.HoldingEntity;
        else
            effectEntity = this;

        return effectEntity;
    }
}