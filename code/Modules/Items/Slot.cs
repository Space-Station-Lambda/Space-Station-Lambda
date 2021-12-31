using Sandbox;
using ssl.Modules.Items.Instances;

namespace ssl.Modules.Items;

public partial class Slot : BaseNetworkable
{
    [Net, Predicted]  public Item Item { get; private set; }

    public bool IsEmpty()
    {
        return Item == null;
    }

    public bool IsFilled()
    {
        return Item != null;
    }

    /// <summary>
    ///     Set an itemstack to the slot
    /// </summary>
    /// <param name="item">Itemstack to set</param>
    public void Set(Item item)
    {
        Item = item;
    }

    public void Clear()
    {
        Item = null;
    }

    public override string ToString()
    {
        return IsEmpty() ? "-" : Item.ToString();
    }
}