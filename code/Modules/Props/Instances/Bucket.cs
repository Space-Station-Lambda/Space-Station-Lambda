using ssl.Modules.Liquids;

namespace ssl.Modules.Props.Instances;

public class Bucket : Prop
{
	public int Capacity { get; set; }

	/// <summary>
	///     The liquid in the bucket.
	/// </summary>
	public Liquid ContainedLiquid { get; } = new();

	/// <summary>
	///     Set the bucket to it's original state with the max amount of water.
	/// </summary>
	public void Wash()
	{
		ContainedLiquid.Clear();
		ContainedLiquid.Add("liquid.water", Capacity);
	}

	/// <summary>
	///     For now the bucken can be waster with a certain amout. He transform the water to waste.
	/// </summary>
	/// <param name="amount">The amount of water to waste</param>
	public void WasteWater( int amount )
	{
		try
		{
			ContainedLiquid.ExtractLiquid("liquid.water", amount);
			ContainedLiquid.Add("liquid.waste", amount);
		}
		catch
		{
			//Remove all water and add waste
			Liquid liquid = ContainedLiquid.Clear("liquid.water");
			ContainedLiquid.Add("liquid.waste", liquid.GetAmount("liquid.water"));
		}
	}
}
