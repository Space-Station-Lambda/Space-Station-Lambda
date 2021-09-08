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
        public Item()
        {
        }

        public Item(ItemData data)
        {
            Data = data;
            
            SetModel(data.Model);
        }

        public virtual ItemData Data { get; }
        
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

        public void OnAction(MainPlayer player, Item item)
        {
            player.Inventory.Add(this);
        }

        /// <summary>
        /// Called when a player use an Item.
        /// </summary>
        public virtual void UseOn(MainPlayer player)
        {
        }

        /// <summary>
        /// Called each player's tick when the Item is considered as the player's active child.
        /// The Item is the active child when the player holds it.
        /// </summary>
        public override void Simulate(Client cl)
        {
            base.Simulate(cl);

            if (Input.Down(InputButton.Attack1)) UseOn(cl.Pawn as MainPlayer);
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
            Data = itemData;
        }

        public override T Data { get; }
        public void OnAction(MainPlayer player, Item item)
        {
            player.Inventory.Add(this);
        }
    }
}