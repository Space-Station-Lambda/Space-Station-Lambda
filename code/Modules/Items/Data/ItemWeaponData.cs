﻿using ssl.Modules.Items.Carriables;

namespace ssl.Modules.Items.Data
{
    /// <summary>
    /// Data of the item weapon
    /// </summary>
    public class ItemWeaponData : ItemData
    {
        private const float DefaultPrimaryRate = 5f;
        private const float DefaultDamage = 10f;
        private const float DefaultRange = 0f;

        public ItemWeaponData(string id, string name, string model, float primaryRate = DefaultPrimaryRate,
            float damage = DefaultDamage, float range = DefaultRange) : base(id, name, model)
        {
            PrimaryRate = primaryRate;
            Damage = damage;
            Range = range;
        }

        /// <summary>
        /// Rate of fire
        /// </summary>
        public float PrimaryRate { get; }

        /// <summary>
        /// Range of the weapon, 0 means max range
        /// </summary>
        public float Range { get; }

        /// <summary>
        /// Damage of the weapon
        /// </summary>
        public float Damage { get; }

        public override ItemWeapon Create()
        {
            return new ItemWeapon(this);
        }
    }
}