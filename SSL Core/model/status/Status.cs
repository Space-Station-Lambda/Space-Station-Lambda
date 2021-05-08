using System;
using System.Collections.Generic;
using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public class Status<T> where T : IEffectable<T>
    {
        public delegate void StatusAwakeEventHandler(Status<T> status);
        public delegate void StatusStartedEventHandler(Status<T> status);
        public delegate void StatusFinishedEventHandler(Status<T> status, StatusFinishedEventArgs statusFinishedEventArgs);

        public event StatusAwakeEventHandler StatusAwake;
        public event StatusStartedEventHandler StatusStarted; 
        public event StatusFinishedEventHandler StatusFinished;
        
        public float SecondsLeft => TotalSeconds - SecondsElapsed;
        public float SecondsElapsed { get; private set; }
        public float TotalSeconds { get; protected set; }

        private List<IEffect<T>> effects;
        
        public Status(float seconds, List<IEffect<T>> effects)
        {
            TotalSeconds = seconds;
            this.effects = effects;
        }

        /// <summary>
        /// Met à jour le temps restant pour le status
        /// </summary>
        protected void UpdateTime(float elapsedTime = 1.0f)
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

        private void ApplyEffects(T affected)
        {
            foreach (var effect in effects)
            {
                affected.Apply(effect);
            }
        }
        public void Update(T affected, float elapsedTime = 1.0f)
        {
            ApplyEffects(affected);
            UpdateTime(elapsedTime);
        }
    }
    
}