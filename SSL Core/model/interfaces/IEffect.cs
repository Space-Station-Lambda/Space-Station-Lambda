namespace SSL_Core.model.interfaces
{
    public interface IEffect<T>
    {
        void Trigger(T affected);
    }
}