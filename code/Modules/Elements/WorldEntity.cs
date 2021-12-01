using Sandbox;

namespace ssl.Modules.Elements
{
    /// <summary>
    /// SSL wrapper for entities that can be spawned in the game.
    /// Inheriting classes are for example Prop or Item.
    /// </summary>
    public abstract partial class WorldEntity : AnimEntity
    {
	    private string model;

	    protected WorldEntity() => Model = "";

	    private string Model
        {
	        get => model;
	        init
	        {
		        model = value;
		        SetModel(model);
	        }
        }
    }
}
