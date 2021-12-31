using Sandbox;

namespace ssl.Modules;

/// <summary>
///     SSL wrapper for entities that can be spawned in the game.
///     Inheriting classes are for example Prop or Item.
/// </summary>
public abstract class WorldEntity : AnimEntity
{
    private string model;

    protected WorldEntity()
    {
        Model = "";
    }

    public string Model
    {
        get => model;
        set
        {
            model = value;
            SetModel(model);
        }
    }
}