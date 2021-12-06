namespace ssl.Factories;

public interface IFactory<out T>
{
	public T Create( string id );
}
