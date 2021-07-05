namespace ssl.Interfaces
{
    public interface IEffectable<T>
    {
        void Apply(IEffect<T> effect);
    }
}