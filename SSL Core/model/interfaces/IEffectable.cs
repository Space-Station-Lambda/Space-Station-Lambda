namespace SSL_Core.model.interfaces
{
    public interface IEffectable<T>
    {
        void Apply(IEffect<T> effect);
    }
}