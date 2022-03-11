namespace ssl.Commons;

public class BaseData
{
    public BaseData(string id)
    {
        Id = id;
    }

    /// <summary>
    ///     Use to identify stuff.
    ///     <example>"item.banana"</example>
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Health of the entity (its durability).
    /// </summary>
    public int Health { get; set; } = 100;

    /// <summary>
    ///     Destroyed when enough damage has been dealt.
    /// </summary>
    public bool Destroyable { get; set; } = false;
}