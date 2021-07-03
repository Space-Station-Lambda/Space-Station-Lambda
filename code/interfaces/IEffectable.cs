namespace SSL.Interfaces
{
    public interface IEffectable<T>
    {
        void Apply(IEffect<T> effect);
    }
}