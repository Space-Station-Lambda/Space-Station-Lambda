using ssl.Data;

namespace ssl.Dao;

public interface IDao<T> where T : BaseData
{
	void Save( T data );
	void Update( T t );
	void Delete( T t );
	T FindById( string id );
	T[] FindAll();
	string[] FindAllIds();
	int Count();
}
