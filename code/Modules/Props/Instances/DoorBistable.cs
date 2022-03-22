using Sandbox;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

/// <summary>
/// Simple bistable door that can can switch between two states (Open and Closed).
///
/// The AnimGraph of the model needs to have a boolean parameter "opened" that represents in which state the door should be in. 
/// </summary>
[Library("ssl_door_bistable")]
public class DoorBistable : Prop
{
    private const float INTERACTION_DELAY = 1F;
    private TimeSince lastUsed;
    public bool Opened => GetAnimParameterBool("opened");

    public override void OnInteract(SslPlayer sslPlayer, int strength)
    {
        if (!(lastUsed > INTERACTION_DELAY)) return;
        
        if (Opened)
        {
            Close();
        }
        else
        {
            Open();
        }

        lastUsed = 0;
    }

    [Input]
    private void Open()
    {
        SetAnimParameter("opened", true);
    }

    [Input]
    private void Close()
    {
        SetAnimParameter("opened", false);
    }
}