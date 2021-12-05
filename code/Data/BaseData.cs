namespace ssl.Data;

public class BaseData
{
	public BaseData( string id )
	{
		Id = id;
	}

	/// <summary>
	///     Use to identify stuff.
	///     <example>"item.banana"</example>
	/// </summary>
	public string Id { get; set; }
}
