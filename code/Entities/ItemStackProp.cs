using Sandbox;
using ssl.Item;

namespace ssl.Entities
{
    /// <summary>
    /// Prop of an ItemStack spawned when an item is in the world.
    /// For example when a player drops an item from their inventory.
    /// </summary>
    public partial class ItemStackProp : Prop
    {
        public ItemStack ItemStack { get; private set; }

        public ItemStackProp() { }
        
        public ItemStackProp(ItemStack itemStack) : this()
        {
            ItemStack = itemStack;
            SetupModel();
        }

        protected void SetupModel()
        {
            SetModel(ItemStack.Item.Model);
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
        }
    }
}