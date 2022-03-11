namespace ssl.Modules.Items;

/// <summary>
///     Different ways for an input to be interpreted.
/// </summary>
public enum InputType
{
    /// <summary>
    ///     The input was not pressed down and now is.
    /// </summary>
    Pressed,
    /// <summary>
    ///     The input is pressed down (whether it was or not before).
    /// </summary>
    Down,
    /// <summary>
    ///     The input is not pressed although it was before.
    /// </summary>
    Released
}