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

	/// <summary>
	/// Get the base type of the data
	/// </summary>
	/// <example>"prop"</example>
	/// <returns></returns>
	public string GetBaseTypeId()
	{
		string [] split = Id.Split( Identifiers.Separator );
		return split[0];
	}
	
	/// <summary>
	/// Get the type of the data.
	/// </summary>
	/// <example>"machine"</example>
	/// <returns></returns>
	public string GetTypeId()
	{
		string [] split = Id.Split( Identifiers.Separator );
		return split[1];
	}
}
