namespace SSL_Core.interfaces
{
    public interface IEffect<T>
    {
        void Trigger(T affected);
    }
}