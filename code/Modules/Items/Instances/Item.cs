using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Component;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;
using ssl.Player.Animators;

namespace ssl.Modules.Items.Instances;

/// <summary>
///     Base class of any Item entity.
///     It is both the item in inventory and the world entity.
///     This class is used clientside and server side so properties useful clientside should be [Net].
/// </summary>
[Library("ssl_item")]
public partial class Item : Carriable, IDraggable
{
    public const string TAG = "Item";

    protected const string HOLD_TYPE_KEY = "holdtype";
    protected const string HANDEDNESS_KEY = "holdtype_handedness";

    [BindComponent] public Glow Glow { get; }

    public override void Spawn()
    {
        base.Spawn();
        Tags.Add(TAG);
        Components.Create<Glow>();
        Glow.Color = Color.Blue;
    }

    [Net, Property] public string Description { get; set; }

    [Net, Property] public string WasteId { get; set; }

    [Net, Property] public HoldType HoldType { get; set; }

    public void OnSelectStart(SslPlayer sslPlayer)
    {
        if (Host.IsClient && IsValid) Glow.Active = true;
    }

    public void OnSelectStop(SslPlayer sslPlayer)
    {
        if (Host.IsClient && IsValid) Glow.Active = false;
    }

    public void OnSelect(SslPlayer sslPlayer) { }

    public virtual void OnInteract(SslPlayer sslPlayer, int strength, TraceResult hit)
    {
        sslPlayer.Inventory.Add(this);
    }

    public virtual void OnDragStart(SslPlayer sslPlayer) { }

    public virtual void OnDragStop(SslPlayer sslPlayer) { }

    public virtual void OnDrag(SslPlayer sslPlayer) { }

    public virtual bool IsDraggable(SslPlayer sslPlayer)
    {
        return true;
    }

    /// <summary>
    ///     Called when a player use an Item.
    /// </summary>
    public virtual void OnDownUsePrimary(SslPlayer sslPlayer, ISelectable target) { }

    public virtual void OnPressedUsePrimary(SslPlayer sslPlayer, ISelectable target) { }
    
    public virtual void OnHoldUsePrimary(SslPlayer sslPlayer, ISelectable target) { }

    public virtual void OnUseSecondary(SslPlayer sslPlayer, ISelectable target) { }

    public override void ActiveStart(Entity ent)
    {
        base.ActiveStart(ent);
        if (ent is not SslPlayer) return;
        UpdateHandViewModel(HoldType);      
    }

    public override void ActiveEnd(Entity ent, bool dropped)
    {
        base.ActiveEnd(ent, dropped);
        if (ent is not SslPlayer) return;
        UpdateHandViewModel(HoldType.None);
    }

    public virtual void SimulateAnimator(HumanAnimator animator)
    {
        animator.SetAnimParameter(HOLD_TYPE_KEY, (int) HoldType);
        animator.SetAnimParameter(HANDEDNESS_KEY, 1);
    }

    private protected override void SaveToDao()
    {
        if (!CanSaveToDao(ItemDao.Instance, this)) return;

        ItemData itemData = new(Id)
        {
            Name = Name,
            Description = Description,
            HoldType = HoldType,
            Model = Model.Name,
            WasteId = WasteId
        };
        
        ItemDao.Instance.Save(itemData);
    }

    [ClientRpc]
    private void UpdateHandViewModel(HoldType holdType)
    {
        if (Local.Pawn is not SslPlayer player) return;
        if (Local.Client != Client) return;
        if (!this.IsValid() || holdType == HoldType.None)
        {
            player.Inventory.ViewModel.RemoveHoldingEntity();
            player.Inventory.ViewModel.SetHoldType(HoldType.None);
        }
        else
        {
            player.Inventory.ViewModel.SetHoldingEntity(this);
            player.Inventory.ViewModel.SetHoldType(holdType);
        }
    }
}