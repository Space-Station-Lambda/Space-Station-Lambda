using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualBasic;
using ssl.Data;

namespace ssl.Dao;

/// <summary>
/// We want to load all the data at the start of the server
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AbstractDao<T> : IDao<T> where T : Data.BaseData
{
    /// <summary>
    /// We store all data in the start of the server locally
    /// </summary>
    protected abstract Dictionary<string, T> Datas { get; set; }
    
    /// <summary>
    /// Save the new data to the local storage
    /// </summary>
    public void Save(T data)
    {
        Datas.Add(data.Id, data);
    }

    /// <summary>
    /// Update a data in the local storage
    /// </summary>
    /// <param name="data"></param>
    public void Update(T data)
    {
        Datas[data.Id] = data;
    }

    /// <summary>
    /// Delete a data from the local storage
    /// </summary>
    /// <param name="data"></param>
    public void Delete(T data)
    {
        Datas.Remove(data.Id);
    }

    /// <summary>
    /// Find a data by its id
    /// </summary>
    public T FindById(string id)
    {
        return Datas[id];
    }

    /// <summary>
    /// Find all data 
    /// </summary>
    public T[] FindAll()
    {
        return Datas.Values.ToArray();
    }

    /// <summary>
    /// Count all data
    /// </summary>
    public int Count()
    {
        return Datas.Count;
    }

    /// <summary>
    /// Load data and create the local storage
    /// </summary>
    protected abstract void LoadAll();
}