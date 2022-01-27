namespace ssl.Commons;

public interface IDao<T> where T : BaseData
{
    void Save(T data);
    void Update(T t);
    void Delete(T t);
    T FindById(string id);
    T[] FindAll();
    string[] FindAllIds();
    bool ContainsId(string id);
    int Count { get; }
}