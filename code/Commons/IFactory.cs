namespace ssl.Commons;

public interface IFactory<out T>
{
    public T Create(string id);
}