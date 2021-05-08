namespace SSL_Core.model.interfaces
{
    public interface IEffect<T>
    {
        void Apply(T affected);
    }
}