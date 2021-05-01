using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public abstract class Status<T> where T : IEffectable
    {
        public uint TicksLeft { get; protected set; }

        public Status(uint totalTicks)
        {
            TicksLeft = totalTicks;
        }

        /// <summary>
        /// Mets à jour les ticks restants pour le status
        /// </summary>
        /// <param name="amount"></param>
        public bool UpdateTick(uint amount = 1)
        {
            if (amount >= TicksLeft)
            {
                TicksLeft = 0;
                return true;
            }
            else
            {
                TicksLeft -= amount;
                return false;
            }
        }
        
        /// <summary>
        /// Applique les effets sur l'entité cible
        /// </summary>
        /// <param name="affected"></param>
        public abstract void Update(T affected);
    }
    
}