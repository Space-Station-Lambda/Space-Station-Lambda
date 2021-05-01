using System;
using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public abstract class Status<T> where T : IEffectable
    {
        public event EventHandler StatusStarted; 
        public event EventHandler<StatusFinishedEventArgs> StatusFinished;
        
        
        public float SecondsLeft { get; protected set; }
        public float SecondsElapsed { get; private set; }
        
        public Status(float seconds)
        {
            SecondsLeft = seconds;
        }

        /// <summary>
        /// Met à jour le temps restant pour le status
        /// </summary>
        public void UpdateTime(float elapsedTime = 1)
        {
            if (SecondsLeft <= 0)
            {
                StatusFinishedEventArgs eventArgs = new StatusFinishedEventArgs(SecondsElapsed);
                
                StatusFinished?.Invoke(this, eventArgs);
            }
            else
            {
                if (SecondsElapsed == 0)
                {
                    StatusStarted?.Invoke(this, EventArgs.Empty);
                }
                
                SecondsLeft -= elapsedTime;
                SecondsElapsed += elapsedTime;
            }
        }
        
        /// <summary>
        /// Applique les effets sur l'entité cible
        /// </summary>
        public abstract void Update(T affected);
    }
    
}