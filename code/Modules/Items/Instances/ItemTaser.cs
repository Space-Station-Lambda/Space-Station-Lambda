using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.ScreenShake;
using ssl.Player;

namespace ssl.Modules.Items.Instances;

public partial class ItemTaser : ItemWeapon
{
    protected override float BulletSpread { get; set; } = 0F;
    protected override float BulletForce { get; set; } = 0F;
    protected override float BulletSize { get; set; } = 1F;

    protected override void ShootBullet( float spread, float force, float bulletSize )
    {
        base.ShootBullet(spread, force, bulletSize);
        if (HitEntity is SslPlayer player)
        {
            player.RagdollHandler.StartRagdoll();
        }
    }
    
    [ClientRpc]
    protected override void FireEffects()
    {
        if (!Host.IsClient)
        {
            return;
        }

        Entity effectEntity;

        if (IsLocalPawn && Local.Pawn is SslPlayer player)
        {
            effectEntity = player.Inventory.ViewModel.HoldingEntity;
            _ = new Perlin();
        }
        else
        {
            effectEntity = this;
        }

        Sound.FromEntity(ShootSound, effectEntity);
    }
}