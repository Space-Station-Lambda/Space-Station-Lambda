namespace ssl.Effects
{
    public abstract class Effect<T>
    {
        public abstract void Trigger(T affected);
    }
}