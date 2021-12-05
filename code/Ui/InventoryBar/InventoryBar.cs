using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.InventoryBar;

public class InventoryBar : Panel
{
	private const int DefaultSlot = 0;


	private readonly InventoryBarSlot[] icons = new InventoryBarSlot[10];
	private int selected;

	public InventoryBar()
	{
		StyleSheet.Load("Ui/InventoryBar/InventoryBar.scss");

		for ( int i = 0; i < 10; i++ )
		{
			string name = (i + 1).ToString();
			if ( i == 9 )
			{
				name = "0";
			}

			icons[i] = new InventoryBarSlot(i, name, this);
		}
	}

	private SslPlayer SslPlayer => (SslPlayer)Local.Pawn;

	private void OnSlotSelected( int newSelected )
	{
		if ( newSelected == selected )
		{
			return;
		}

		//If the previous was in range
		if ( 0 <= selected && icons.Length > selected )
		{
			icons[selected].SetClass("selected", false);
		}

		//If the new is in range
		if ( 0 <= newSelected && icons.Length > newSelected )
		{
			icons[newSelected].SetClass("selected", true);
			icons[newSelected].RefreshModel();
		}

		selected = newSelected;
	}

	private void RefreshAllModels()
	{
		foreach ( InventoryBarSlot icon in icons )
		{
			icon.RefreshModel();
		}
	}

	public override void Tick()
	{
		base.Tick();
		OnSlotSelected(SslPlayer.Inventory.HoldingSlotNumber);
		RefreshAllModels();
	}
}
