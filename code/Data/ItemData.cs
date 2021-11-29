namespace ssl.Data;

public class ItemData : BaseData
{
    public ItemData(string id) : base("item." + id)
    {
    }

    /// <summary>
    /// Name of the item. 
    /// <example>"Banana"</example>
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Description of the item.
    /// <example>"Used to eat. Are you a monkey ?"</example>
    /// </summary>
    public string Description { get; set; } = "";
    
    /// <summary>
    /// Model of the item.
    /// <example>"path/model"</example>
    /// </summary>
    public string Model { get; set; } = "ERROR";

    /// <summary>
    /// How the player hold the item
    /// TODO Add the holdtype enum
    /// <example>"HoldTypes.Weapon"</example>
    /// </summary>
    public int HoldType { get; set; } = 1;

   
}