using Sandbox;

namespace ssl.Modules.Elements
{
    /// <summary>
    /// SSL wrapper for entities that can be spawned in the game.
    /// Inheriting classes are for example Prop or Item.
    /// </summary>
    public abstract partial class WorldEntity : AnimEntity
    {
        protected WorldEntity()
        {
        }

        public WorldEntity(BaseData data)
        {
            SetModel(data.Model);
            Data = data;
        }
        
        [Net] protected BaseData Data { get; set; }
    }
}