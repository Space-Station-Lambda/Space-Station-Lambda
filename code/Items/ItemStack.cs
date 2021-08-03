using System;
using Sandbox;
using ssl.Items.Data;
using ssl.Player;

namespace ssl.Items
{
    public partial class ItemStack : BaseCarriable
    {
        private Item item;

        public ItemStack()
        {
        }

        public ItemStack(Item item)
        {
            Item = item;
            Item.OnInit(this);
        }

        public ItemStack(ItemStack itemStack) : this(itemStack.item)
        {
        }

        public override string ViewModelPath => item.ViewModelPath;

        public Item Item
        {
            get => item;
            private set
            {
                item = value;
                _itemId = value.Id;
            }
        }

        // This property exists to allow Item to be networked by its id only

        // The _ is here to have a better callback method name since it is a private field
        [Net, OnChangedCallback] private string _itemId { get; set; }
        [Net, Predicted] public StackData Data { get; set; }

        public override void Spawn()
        {
            base.Spawn();
            
            CollisionGroup = CollisionGroup.Weapon; // so players touch it as a trigger but not as a solid
            SetInteractsAs(CollisionLayer.Debris); // so player movement doesn't walk into it
        }

        public override void Simulate(Client cl)
        {
            base.Simulate(cl);
            item.OnSimulate(this);
        }
        
        

        /// <summary>
        /// Callback executed to keep the Item property synceds
        /// </summary>
        private void On_itemIdChanged()
        {
            Item = Item.All[_itemId];
        }

        public override string ToString()
        {
            return $"{Item}";
        }
    }
}