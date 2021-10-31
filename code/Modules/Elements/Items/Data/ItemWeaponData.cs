﻿using Sandbox;

namespace ssl.Modules.Elements.Items.Data
{
    /// <summary>
    /// Stores data to create an Item instance.
    /// </summary>
    [Library("weapon")]
    public class ItemWeaponData : ItemData
    {
        public float PrimaryRate { get; set; } = 5f;
        public float Damage { get; set; } = 10f;
        public float Range { get; set; } = 0f;
        public string ShootSound { get; set; } = "";
        public string MuzzleFlashParticle { get; set; } = "";
    }
}