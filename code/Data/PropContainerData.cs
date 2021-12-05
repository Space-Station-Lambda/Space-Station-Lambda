namespace ssl.Data;

public class PropContainerData : PropData
{
	public PropContainerData( string id ) : base("prop." + id)
	{
	}

	public int Capacity { get; set; }
}
