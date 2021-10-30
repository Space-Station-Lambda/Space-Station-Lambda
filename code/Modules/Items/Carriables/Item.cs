﻿using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;
using ssl.Player.Animators;

namespace ssl.Modules.Items.Carriables
{
    /// <summary>
    /// Base class of any Item entity.
    /// It is both the item in inventory and the world entity.
    /// This class is used clientside and server side so properties useful clientside should be [Net].
    /// </summary>
    public partial class Item : WorldEntity, IDraggable
    {
        public const string Tag = "Item";
        
        protected const string HoldTypeKey = "holdtype";
        protected const string HandednessKey = "holdtype_handedness";

        public Item()
        {
        }

        public Item(ItemData data) : base(data)
        {
            Tags.Add(Tag);
            GlowColor = Color.Blue;
        }

        /// <summary>
        /// The PhysicsBody used when the Item will be dragged.
        /// By default it's only the default PhysicsBody.
        /// </summary>
        public virtual PhysicsBody Body => PhysicsBody;

        public new ItemData Data => (ItemData)base.Data;
        
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
        
        public virtual void OnDragStart(MainPlayer player)
        {
        }

        public virtual void OnDragStop(MainPlayer player)
        {
        }

        public virtual void OnDrag(MainPlayer player)
        {
        }
        
        public virtual bool IsDraggable(MainPlayer player)
        {
            return true;
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


        public virtual void SimulateAnimator(HumanAnimator animator)
        {
            animator.SetParam(HoldTypeKey, Data.HoldType);
            animator.SetParam(HandednessKey, 1);
        }

        public override string ToString()
        {
            return Data.ToString();
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
