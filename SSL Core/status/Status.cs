using System.Collections.Generic;
using SSL_Core.interfaces;

namespace SSL_Core.status
{
    public class Status<T> where T : IEffectable<T>
    {

        public delegate void StatusFinishedEventHandler(Status<T> status, float secondsElapsed);
        
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
        
        public void Update(T affected, float elapsedTime = 1.0f)
        {
            SecondsElapsed += elapsedTime;
            
            if (SecondsLeft <= 0)
            {
                StatusFinished?.Invoke(this, SecondsElapsed);
            }

            foreach (var effect in effects)
            {
                affected.Apply(effect);
            }
        }
    }
    
}