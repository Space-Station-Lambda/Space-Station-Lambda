﻿using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    /// <summary>
    /// Base class of any Item entity.
    /// It is both the item in inventory and the world entity.
    /// This class is used clientside and server side so properties useful clientside should be [Net].
    /// </summary>
    public partial class Item : Carriable, ISelectable
    {
        public Item()
        {
        }

        public Item(ItemData data)
        {
            Data = data;
            SetModel(data.Model);
            GlowColor = Color.Blue;
        }

        [Net] public ItemData Data { get; private set; }
        
        public void OnSelectStart(MainPlayer player)
        {
            if (Host.IsClient) GlowActive = true;
        }

        public void OnSelectStop(MainPlayer player)
        {
            if (Host.IsClient) GlowActive = false;
        }

        public void OnSelect(MainPlayer player)
        {
            //TODO
        }

        public void OnInteract(MainPlayer player)
        {
            player.Inventory.Add(this);
        }

        /// <summary>
        /// Called when a player use an Item.
        /// </summary>
        public virtual void OnUsePrimary(MainPlayer player, ISelectable target)
        {
        }
        
        public virtual void OnUseSecondary(MainPlayer player, ISelectable target)
        {
        }

        public override void ActiveStart(Entity ent)
        {
            base.ActiveStart(ent);

            if (!Host.IsClient || ent is not MainPlayer player) return;
            player.Inventory.ViewModel.SetHoldingEntity(this);
            player.Inventory.ViewModel.SetHoldType((HoldType)Data.HoldType);
        }

        public override void ActiveEnd(Entity ent, bool dropped)
        {
            base.ActiveEnd(ent, dropped);

            if (!Host.IsClient || ent is not MainPlayer player) return;
            player.Inventory.ViewModel.RemoveHoldingEntity();
            player.Inventory.ViewModel.SetHoldType(HoldType.None);
        }

        public override string ToString()
        {
            return $"[{Data.Id}] {Data.Name}";
        }

        public override int GetHashCode()
        {
            return (Data.Id != null ? Data.Id.GetHashCode() : 0);
        }
    }

    public class Item<T> : Item where T : ItemData
    {
        public Item()
        {
        }

        public Item(T itemData) : base(itemData)
        {
        }

        public new T Data => (T)base.Data;
    }
}