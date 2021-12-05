namespace ssl.Modules.Items.Data;

public class ItemCleanerData : ItemData
{
	public ItemCleanerData( string id ) : base("cleaner." + id)
	{
	}

	/// <summary>
	///     Cleaning value of the current cleaner
	///     <example>10</example>
	/// </summary>
	public int CleaningValue { get; set; } = 10;
}
