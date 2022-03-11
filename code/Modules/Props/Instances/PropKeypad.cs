using System;
using System.Text.Json.Serialization;
using Sandbox;
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

    public BBox BoundingBox => new(BoxMin, BoxMax);

    [JsonPropertyName("button_min")]
    public Vector3 BoxMin { get; set; }

    [JsonPropertyName("button_max")]
    public Vector3 BoxMax { get; set; }
}

/// <summary>
/// Entity for all keypad like machines.
/// </summary>
[Library("ssl_prop_keypad")]
public partial class PropKeypad : Prop
{
    /// <summary>
    ///     Fired when a button is pressed.
    /// </summary>
    protected Output<string> OnKeyPressed { get; set; }

    protected KeypadButton[] Buttons { get; set; }
    
    public override void Spawn()
    {
        base.Spawn();
        Buttons = Model.GetData<KeypadButton[]>();
    }

    public override void OnInteract(SslPlayer sslPlayer, int strength, TraceResult hit)
    {
        base.OnInteract(sslPlayer, strength, hit);

        if (!Host.IsServer) return;
        
        Vector3 localHitPos = hit.EndPosition - Transform.Position;
        KeypadButton button = GetButtonFromLocalPos(localHitPos);
        KeyPressed(sslPlayer, button?.KeyValue);
    }

    protected virtual void KeyPressed(Entity presser, string key) { }

    private KeypadButton GetButtonFromLocalPos(Vector3 localPos)
    {
        BBox hitBox = new(localPos);
        
        foreach (KeypadButton button in Buttons)
        {
            Vector3 min = button.BoxMin * Transform.Rotation * Transform.Scale;
            Vector3 max = button.BoxMax * Transform.Rotation * Transform.Scale;
            
            Vector3 adjMin = new(MathF.Min(min.x, max.x), MathF.Min(min.y, max.y), MathF.Min(min.z, max.z));
            Vector3 adjMax = new(MathF.Max(min.x, max.x), MathF.Max(min.y, max.y), MathF.Max(min.z, max.z));
            
            BBox adjustedBBox = new(adjMin, adjMax);
            if (adjustedBBox.Contains(hitBox)) return button;
        }

        return null;
    }
}