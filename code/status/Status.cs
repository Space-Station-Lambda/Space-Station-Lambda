using System.Collections.Generic;
using SSL.Interfaces;

namespace SSL.Status
{
    public class Status<T> where T : IEffectable<T>
    {
        public delegate void StatusFinishedEventHandler(Status<T> status, float secondsElapsed);

        private List<IEffect<T>> effects;

        public Status(float millis, List<IEffect<T>> effects)
        {
            TotalMillis = millis;
            this.effects = effects;
        }

        public float MillisLeft => TotalMillis - MillisElapsed;
        public float MillisElapsed { get; private set; }
        public float TotalMillis { get; protected set; }

        public event StatusFinishedEventHandler StatusFinished;

        public void Update(T affected, float elapsedTime = 1000.0f)
        {
            MillisElapsed += elapsedTime;

            if (MillisLeft <= 0)
            {
                StatusFinished?.Invoke(this, MillisElapsed);
            }

            foreach (var effect in effects)
            {
                affected.Apply(effect);
            }
        }
    }
}