using System;

namespace ssl.Modules.Skills;

/// <summary>
///     A specific skill value. We don't use the experience system for this because wed don't really want to exp.
/// </summary>
public class Skill
{
    private int level;

    /// <summary>
    ///     Id of the skill
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     The name of the skill. Commonly used in UI or in console.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Description of the skill. Commonly used in UI or in console.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Level of the skill. Minimum is 0 and maximum is 100.
    /// </summary>
    public int Level
    {
        get => level;
        set => level = Math.Clamp(value, 0, 100);
    }
}