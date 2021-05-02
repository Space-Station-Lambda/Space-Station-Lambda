using System;
using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public abstract class Status<T> where T : IEffectable
    {
        public delegate void StatusStartedEventHandler(Status<T> status);
        public delegate void StatusFinishedEventHandler(Status<T> status, StatusFinishedEventArgs statusFinishedEventArgs);
        
        public event StatusStartedEventHandler StatusStarted; 
        public event StatusFinishedEventHandler StatusFinished;
        
        public float SecondsLeft => TotalSeconds - SecondsElapsed;
        public float SecondsElapsed { get; private set; }
        public float TotalSeconds { get; protected set; }
        
        public Status(float seconds)
        {
            TotalSeconds = seconds;
        }

        /// <summary>
        /// Met à jour le temps restant pour le status
        /// </summary>
        public void UpdateTime(float elapsedTime = 1.0f)
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
                    StatusStarted?.Invoke(this);
                }
                
                SecondsElapsed += elapsedTime;
            }
        }
        
        /// <summary>
        /// Applique les effets sur l'entité cible
        /// </summary>
        public abstract void Update(T affected);
    }
    
}