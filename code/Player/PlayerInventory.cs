using System;
using Sandbox;
using ssl.Modules.Items;
using ssl.Modules.Items.Instances;

namespace ssl.Player;

public partial class PlayerInventory : Inventory
{
	private const int MAX_INVENTORY_CAPACITY = 10;

	public PlayerInventory() : base(MAX_INVENTORY_CAPACITY, new ItemFilter())
	{
	}

	[Net] [Predicted] public int HoldingSlotNumber { get; private set; } = -1;
	public Item HoldingItem => HoldingSlot?.Item;
	public Slot HoldingSlot { get; private set; }
	private SslPlayer SslPlayer => (SslPlayer)Entity;
	public HandViewModel ViewModel { get; set; }

	public void ProcessHolding( int slotIndex )
	{
		if ( HoldingSlotNumber == slotIndex )
		{
			StopHolding();
		}
		else
		{
			StartHolding(Slots[slotIndex]);
		}
	}

	private void StartHolding( Slot slot )
	{
		StopHolding();
		HoldingSlot = slot;
		HoldingItem?.ActiveStart(SslPlayer);
		SslPlayer.ActiveChild = HoldingItem;
		HoldingSlotNumber = Slots.IndexOf(slot);
	}

	private void StopHolding()
	{
		HoldingItem?.ActiveEnd(SslPlayer, false);
		HoldingSlot = null;
		SslPlayer.ActiveChild = null;
		HoldingSlotNumber = -1;
	}

	public Item DropItem()
	{
		Item droppedItem = HoldingItem;
		HoldingItem?.ActiveEnd(SslPlayer, true);
		HoldingItem?.OnCarryDrop(SslPlayer);
		HoldingSlot.Clear();
		return droppedItem;
	}

	/// <summary>
	///     Adds an ItemStack to a preferred position in the inventory.
	/// </summary>
	/// The ItemStack will be merged if the preferred position is the same Item.
	/// If the preferred position is not the same Item, it will add the stack to the next available slot.
	/// <param name="item">Item stack to add</param>
	/// <param name="position">The preferred position</param>
	/// <exception cref="IndexOutOfRangeException">If the specified position is out of bounds.</exception>
	public override Slot Add( Item item, int position = 0 )
	{
		Slot destinationSlot = base.Add(item, position);
		item.OnCarryStart(SslPlayer);
		if ( null == destinationSlot )
		{
			item.OnCarryDrop(item);
		}

		if ( destinationSlot == HoldingSlot )
		{
			StartHolding(HoldingSlot);
		}

		return destinationSlot;
	}

	public void UsePrimary()
	{
		HoldingItem?.OnUsePrimary(SslPlayer, SslPlayer.Dragger.Selected);
	}

	public void UseSecondary()
	{
		HoldingItem?.OnUseSecondary(SslPlayer, SslPlayer.Dragger.Selected);
	}

	protected override void OnDeactivate()
	{
		if ( Host.IsClient )
		{
			ViewModel?.Delete();
		}
	}
}
