using ssl.Commons;
using ssl.Player;

namespace ssl.Modules.Items.Data;

public class ItemData : BaseData
{
    public ItemData(string id) : base(id) { }

    /// <summary>
    ///     Name of the item.
    ///     <example>"Banana"</example>
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    ///     Description of the item.
    ///     <example>"Used to eat. Are you a monkey ?"</example>
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    ///     Model of the item.
    ///     <example>"path/model"</example>
    /// </summary>
    public string Model { get; set; } = "ERROR";

    /// <summary>
    ///     Id of wasted item. Idk if and ItemData is better.
    ///     <example>"peau_de_banane"</example>
    /// </summary>
    public string WasteId { get; set; } = "";

    /// <summary>
    ///     How the player hold the item
    ///     <example>"HoldTypes.Weapon"</example>
    /// </summary>
    public HoldType HoldType { get; set; } = HoldType.Hand;
}