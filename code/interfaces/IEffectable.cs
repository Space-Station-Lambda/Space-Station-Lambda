namespace SSL.interfaces
{
    public interface IEffectable<T>
    {
        void Apply(IEffect<T> effect);
    }
}