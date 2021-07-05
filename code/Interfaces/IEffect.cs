namespace ssl.Interfaces
{
    public interface IEffect<T>
    {
        void Trigger(T affected);
    }
}