using System.Collections.Generic;
using ssl.Interfaces;

namespace ssl.Status
{
    public class StatusHandler<T> where T : IEffectable<T>
    {
        private List<Status<T>> statuses;

        public StatusHandler()
        {
            statuses = new List<Status<T>>();
        }

        public int StatusCount => statuses.Count;

        /// <summary>
        /// Updates all the statuses
        /// </summary>
        /// <param name="affected">Affected entity by the statuses</param>
        public void Update(T affected, float elapsed = 1.0f)
        {
            for (int i = 0; i < StatusCount; ++i)
            {
                Status<T> status = statuses[i];
                status.Update(affected, elapsed);
            }
        }

        public void AddStatus(Status<T> status)
        {
            SubscribeEvent(status);

            statuses.Add(status);
        }

        public void RemoveStatus(Status<T> status)
        {
            UnSubscribeEvent(status);

            statuses.Remove(status);
        }

        private void SubscribeEvent(Status<T> status)
        {
            status.StatusFinished += OnStatusFinished;
        }

        private void UnSubscribeEvent(Status<T> status)
        {
            status.StatusFinished -= OnStatusFinished;
        }

        /// <summary>
        /// When a status comes to its end, it is removed
        /// </summary>
        private void OnStatusFinished(Status<T> status, float elapsedTime)
        {
            RemoveStatus(status);
        }
    }
}