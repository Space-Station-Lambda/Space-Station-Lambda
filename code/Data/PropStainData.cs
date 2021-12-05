namespace ssl.Data;

public class PropStainData : PropData
{
	
	public PropStainData(string id) : base("stain." + id)
	{ }
	
	public int StainMaxStrength { get; set; }
}
