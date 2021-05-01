namespace SSL_Core.model.interfaces
{
    /// <summary>
    /// Element updatable a chaque tick de jeu
    /// </summary>
    public interface ITickable
    {
        void Update(float delta = 0);
    }
}