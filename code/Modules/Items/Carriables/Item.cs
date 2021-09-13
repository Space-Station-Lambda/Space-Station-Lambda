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
            Id = data.Id;
            Name = data.Name;
            Model = data.Model;
            Description = data.Description;
            HoldType = (HoldType) data.HoldType;
            
            SetModel(data.Model);
        }

        [Net] public string Id { get; protected set; }
        [Net] public string Name { get; protected set; }
        [Net] public string Model { get; protected set; }
        [Net] public string Description { get; protected set; }
        [Net] public HoldType HoldType { get; protected set; }
        
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
            return $"[{Id}] {Name}";
        }

        protected bool Equals(Item other)
        {
            return Id == other.Id;
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
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}