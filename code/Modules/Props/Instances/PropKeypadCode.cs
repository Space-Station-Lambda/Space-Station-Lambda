using Sandbox;

namespace ssl.Modules.Props.Instances;

[Library("ssl_prop_keypad_code")]
public partial class PropKeypadCode : PropKeypad
{
    public int MaxLength => Code.Length;
    public string Input { get; set; } = "";
    [Property] public string Code { get; set; }
    
    /// <summary>
    ///     Fired when the enter button is pressed with the Input.
    /// </summary>
    protected Output<string> OnEnterPressed { get; set; }
    protected Output OnCorrectCode { get; set; }
    
    protected override void KeyPressed(Entity presser, string key)
    {
        OnKeyPressed.Fire(presser, key);
        
        // TODO: Show input in the world

        switch (key)
        {
            case "enter":
                EnterPressed(presser, Input);
                break;
            
            case "clear":
                Input = string.Empty;
                break;
            
            default:
                if (Input.Length < MaxLength) Input += key;
                break;
        }

        Log.Info($"{key} pressed - Keypad current input: {Input}");
    }

    protected void EnterPressed(Entity presser, string value)
    {
        OnEnterPressed.Fire(presser, value);
        if (Input.Equals(Code)) CorrectInput(presser);
        Input = string.Empty;
    }

    protected void CorrectInput(Entity presser)
    {
        OnCorrectCode.Fire(presser);
    }
}