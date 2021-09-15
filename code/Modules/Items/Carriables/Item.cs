using Sandbox;
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
        protected Item()
        {
        }

        public Item(ItemData data)
        {
            Data = data;
            SetModel(data.Model);
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

        public override string ToString()
        {
            return $"[{Data.Id}] {Data.Name}";
        }

        protected bool Equals(Item other)
        {
            return Data.Id == other.Data.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Item)obj);
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