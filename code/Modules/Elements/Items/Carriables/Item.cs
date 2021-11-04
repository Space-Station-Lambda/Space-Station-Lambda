using Sandbox;
using ssl.Modules.Elements.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;
using ssl.Player.Animators;

namespace ssl.Modules.Elements.Items.Carriables
{
    /// <summary>
    /// Base class of any Item entity.
    /// It is both the item in inventory and the world entity.
    /// This class is used clientside and server side so properties useful clientside should be [Net].
    /// </summary>
    public partial class Item : Carriable, IDraggable
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

        public new ItemData Data => (ItemData)base.Data;
        
        public void OnSelectStart(Player.SslPlayer sslPlayer)
        {
            if (Host.IsClient) GlowActive = true;
        }

        public void OnSelectStop(Player.SslPlayer sslPlayer)
        {
            if (Host.IsClient) GlowActive = false;
        }

        public void OnSelect(Player.SslPlayer sslPlayer)
        {
            //TODO
        }

        public void OnInteract(Player.SslPlayer sslPlayer)
        {
            sslPlayer.Inventory.Add(this);
        }
        
        public virtual void OnDragStart(Player.SslPlayer sslPlayer)
        {
        }

        public virtual void OnDragStop(Player.SslPlayer sslPlayer)
        {
        }

        public virtual void OnDrag(Player.SslPlayer sslPlayer)
        {
        }
        
        public virtual bool IsDraggable(Player.SslPlayer sslPlayer)
        {
            return true;
        }

        /// <summary>
        /// Called when a player use an Item.
        /// </summary>
        public virtual void OnUsePrimary(Player.SslPlayer sslPlayer, ISelectable target)
        {
        }

        public virtual void OnUseSecondary(Player.SslPlayer sslPlayer, ISelectable target)
        {
        }

        public override void ActiveStart(Entity ent)
        {
            base.ActiveStart(ent);

            if (!Host.IsClient || ent is not Player.SslPlayer player) return;
            player.Inventory.ViewModel.SetHoldingEntity(this);
            player.Inventory.ViewModel.SetHoldType((HoldType)Data.HoldType);
        }

        public override void ActiveEnd(Entity ent, bool dropped)
        {
            base.ActiveEnd(ent, dropped);

            if (!Host.IsClient || ent is not Player.SslPlayer player) return;
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
