using Sandbox;
using ssl.Item;

namespace ssl.Entities
{
    /// <summary>
    /// Prop of an ItemStack spawned when an item is in the world.
    /// For example when a player drops an item from their inventory.
    /// </summary>
    public class ItemStackProp : Prop
    {
        public sealed override Rotation Rotation
        {
            get => base.Rotation;
            set => base.Rotation = value;
        }
        public sealed override Vector3 Position
        {
            get => base.Position;
            set => base.Position = value;
        }
        public ItemStack ItemStack { get; private set; }

        public ItemStackProp()
        {
            
        }
        
        public ItemStackProp(ItemStack itemStack, Vector3 position, Rotation rotation)
        {
            ItemStack = itemStack;
            Position = position;
            Rotation = rotation;
            
            SetModel(ItemStack.Item.Model);
            SetupPhysicsFromModel(PhysicsMotionType.Dynamic);
        }

    }
}