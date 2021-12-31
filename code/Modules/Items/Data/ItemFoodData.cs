namespace ssl.Modules.Items.Data;

/// <summary>
///     Food in game is used for the player to feeding himself.
/// </summary>
public class ItemFoodData : ItemData
{
    public ItemFoodData(string id) : base(id) { }

    /// <summary>
    ///     Feeding value of the current food
    ///     <example>10</example>
    /// </summary>
    public int FeedingValue { get; set; } = 10;
}