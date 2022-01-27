using System.Collections.Generic;
using System.Linq;

namespace ssl.Commons;

/// <summary>
///     We want to load all the data at the start of the server
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class LocalDao<T> : IDao<T> where T : BaseData
{
    protected LocalDao()
    {
        Initialize();
    }

    /// <summary>
    ///     We store all data in the start of the server locally
    /// </summary>
    protected Dictionary<string, T> All { get; set; } = new();

    /// <summary>
    ///     Count all data
    /// </summary>
    public int Count => All.Count;

    /// <summary>
    ///     Save the new data to the local storage
    /// </summary>
    public void Save(T data)
    {
        if (All.ContainsKey(data.Id))
        {
            Log.Error($"{data.Id} already exists in the local storage");
            return;
        }

        All.Add(data.Id, data);
        Log.Info($"{data.Id} preloaded");
    }

    /// <summary>
    ///     Update a data in the local storage
    /// </summary>
    /// <param name="data"></param>
    public void Update(T data)
    {
        All[data.Id] = data;
    }

    /// <summary>
    ///     Delete a data from the local storage
    /// </summary>
    /// <param name="data"></param>
    public void Delete(T data)
    {
        All.Remove(data.Id);
    }

    /// <summary>
    ///     Find a data by its id
    /// </summary>
    public T FindById(string id)
    {
        if (All.ContainsKey(id)) return All[id];

        Log.Error($"{id} not found in the local storage");
        return null;
    }

    /// <summary>
    ///     Find all data
    /// </summary>
    public T[] FindAll()
    {
        return All.Values.ToArray();
    }

    public string[] FindAllIds()
    {
        return All.Keys.ToArray();
    }

    /// <summary>
    ///     Load data and create the local storage
    /// </summary>
    protected abstract void LoadAll();

    private void Initialize()
    {
        LoadAll();
    }
}