namespace ssl.Effects
{
    public interface IEffectable<T>
    {
        void Apply(Effect<T> effect);
    }
}