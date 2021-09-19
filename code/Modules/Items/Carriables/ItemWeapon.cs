﻿using Sandbox;
using Sandbox.ScreenShake;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemWeapon : Item<ItemWeaponData>
    {
        private const string MuzzleAttachmentName = "muzzle";
        private const float MaxRange = 5000;

        public ItemWeapon()
        {
        }

        public ItemWeapon(ItemWeaponData itemData) : base(itemData)
        {
        }

        [Net, Predicted] public TimeSince TimeSincePrimaryAttack { get; set; }

        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            base.OnUsePrimary(player, target);

            if (CanPrimaryAttack())
            {
                AttackPrimary();
            }
        }

        protected bool CanReload()
        {
            return Owner.IsValid() && Input.Down(InputButton.Reload);
        }

        protected void Reload()
        {
        }

        private bool CanPrimaryAttack()
        {
            if (!Owner.IsValid()) return false;
            if (Data.PrimaryRate <= 0) return true;

            return TimeSincePrimaryAttack > (1 / Data.PrimaryRate);
        }

        protected void AttackPrimary()
        {
            TimeSincePrimaryAttack = 0;

            using (Prediction.Off())
            {
                ShootEffects();
            }

            //TODO: Bullet to class
            ShootBullet(0.05f, 1.5f, 3.0f);
        }

        /// <summary>
        /// Does a trace from start to end, does bullet impact effects. Coded as an IEnumerable so you can return multiple
        /// hits, like if you're going through layers or ricocet'ing or something.
        /// </summary>
        protected virtual TraceResult TraceBullet(Vector3 start, Vector3 end, float radius = 2.0f)
        {
            bool inWater = Physics.TestPointContents(start, CollisionLayer.Water);

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

        protected virtual void ShootBullet(float spread, float force, float bulletSize)
        {
            Vector3 forward = Owner.EyeRot.Forward;
            forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
            forward = forward.Normal;

            //If the range is 0, the range is max.
            float range = Data.Range == 0 ? MaxRange : Data.Range;
            TraceResult tr = TraceBullet(Owner.EyePos, Owner.EyePos + forward * range, bulletSize);

            if (!IsServer || !tr.Entity.IsValid()) return;
            tr.Surface.DoBulletImpact(tr);

            DamageInfo damageInfo = DamageInfo.FromBullet(tr.EndPos, forward * 100 * force, Data.Damage)
                .UsingTraceResult(tr)
                .WithAttacker(Owner)
                .WithWeapon(this);

            tr.Entity.TakeDamage(damageInfo);
        }

        [ClientRpc]
        private void ShootEffects()
        {
            if (!Host.IsClient) return;
            
            Entity effectEntity;
            
            if (IsLocalPawn && Local.Pawn is MainPlayer player)
            {
                effectEntity = player.Inventory.ViewModel.HoldingEntity;
                _ = new Perlin();
            }
            else
            {
                effectEntity = this;
            }

            Sound.FromEntity(Data.ShootSound, effectEntity);
            Particles.Create(Data.MuzzleFlashParticle, effectEntity, MuzzleAttachmentName);
        }
    }
}