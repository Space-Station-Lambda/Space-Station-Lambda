using System.Collections.Generic;
using System.Linq;
using ssl.Data;

namespace ssl.Dao;

/// <summary>
///     We want to load all the data at the start of the server
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class LocalDao<T> : IDao<T> where T : BaseData
{

	/// <summary>
	///     We store all data in the start of the server locally
	/// </summary>
	protected abstract Dictionary<string, T> All { get; set; }

	/// <summary>
	///     Save the new data to the local storage
	/// </summary>
	public void Save( T data )
	{
		All.Add(data.Id, data);
		Log.Info($"{data.Id} preloaded");
	}

	/// <summary>
	///     Update a data in the local storage
	/// </summary>
	/// <param name="data"></param>
	public void Update( T data )
	{
		All[data.Id] = data;
	}

	/// <summary>
	///     Delete a data from the local storage
	/// </summary>
	/// <param name="data"></param>
	public void Delete( T data )
	{
		All.Remove(data.Id);
	}

	/// <summary>
	///     Find a data by its id
	/// </summary>
	public T FindById( string id )
	{
		return All[id];
	}

	/// <summary>
	///     Find all data
	/// </summary>
	public T[] FindAll()
	{
		return All.Values.ToArray();
	}

	/// <summary>
	///     Count all data
	/// </summary>
	public int Count()
	{
		return All.Count;
	}

	/// <summary>
	///     Load data and create the local storage
	/// </summary>
	protected abstract void LoadAll();
}
