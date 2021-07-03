namespace SSL.interfaces
{
    public interface IEffect<T>
    {
        void Trigger(T affected);
    }
}