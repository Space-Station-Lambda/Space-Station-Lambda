using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sandbox;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Props.Instances;

/// <summary>
///     Represents a button on a keypad via a box in the model and the string value that it represents.
/// </summary>
[ModelDoc.GameData("keypad_button", AllowMultiple = true)]
[ModelDoc.Box("button_min", "button_max")]
public class KeypadButton
{
    [JsonPropertyName("key_value")]
    public string KeyValue { get; set; }

    [JsonPropertyName("button_min")]
    private Vector3 BoxMin { get; set; }
    
    [JsonPropertyName("button_max")]
    private Vector3 BoxMax { get; set; }
}

/// <summary>
/// Class for all machines that the user can use to type something.
/// </summary>
[Library("ssl_prop_keypad")]
public class PropKeypad : Prop
{
    private KeypadButton[] Buttons { get; set; }
    
    public override void Spawn()
    {
        base.Spawn();
        Buttons = Model.GetData<KeypadButton[]>();
    }

    public override void OnInteract(SslPlayer sslPlayer, int strength)
    {
        base.OnInteract(sslPlayer, strength);
        Log.Info(Buttons.Length);
    }
}