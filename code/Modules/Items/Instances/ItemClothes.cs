using Sandbox;
using ssl.Modules.Items.Instances;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Clothes;

public class ItemClothes : Item
{
    public ClothesSlot Slot { get; set; }
    //TODO: Have real physics with model

    public override void OnPressedUsePrimary(SslPlayer sslPlayer, ISelectable target)
    {
        base.OnPressedUsePrimary(sslPlayer, target);

        Log.Info(sslPlayer.ClothesHandler);
        
        sslPlayer.Inventory.RemoveItem(this);
        sslPlayer.ClothesHandler.AttachClothes(this);
    }
}