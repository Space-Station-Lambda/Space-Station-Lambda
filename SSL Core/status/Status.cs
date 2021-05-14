using System.Collections.Generic;
using SSL_Core.interfaces;

namespace SSL_Core.status
{
    public class Status<T> where T : IEffectable<T>
    {

        public delegate void StatusFinishedEventHandler(Status<T> status, float secondsElapsed);
        
        public event StatusFinishedEventHandler StatusFinished;
        
        public float MillisLeft => TotalMillis - MillisElapsed;
        public float MillisElapsed { get; private set; }
        public float TotalMillis { get; protected set; }

        private List<IEffect<T>> effects;
        
        public Status(float millis, List<IEffect<T>> effects)
        {
            TotalMillis = millis;
            this.effects = effects;
        }
        
        public void Update(T affected, float elapsedTime = 1000.0f)
        {
            if (MillisLeft <= 0)
            {
                StatusFinished?.Invoke(this, MillisElapsed);
            }
            else
            {
                MillisElapsed += elapsedTime;
            }

            foreach (var effect in effects)
            {
                affected.Apply(effect);
            }
        }
    }
    
}