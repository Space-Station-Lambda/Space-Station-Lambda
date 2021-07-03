namespace SSL_Core.interfaces
{
    public interface IEffectable<T>
    {
        void Apply(IEffect<T> effect);
    }
}