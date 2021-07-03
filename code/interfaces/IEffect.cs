namespace SSL.Interfaces
{
    public interface IEffect<T>
    {
        void Trigger(T affected);
    }
}