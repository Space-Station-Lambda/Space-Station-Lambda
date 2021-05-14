using System.Collections.Generic;
using SSL_Core.interfaces;

namespace SSL_Core.status
{
    public class StatusHandler<T> where T : IEffectable<T>
    {
        public int StatusCount => statuses.Count;
        
        private List<Status<T>> statuses;

        public StatusHandler()
        {
            statuses = new List<Status<T>>();
        }
        
        /// <summary>
        /// Actualise tous les status
        /// </summary>
        /// <param name="affected">Entité affectée par les status</param>
        public void Update(T affected, float elapsed = 1f)
        {
            for (int i=0;i<StatusCount;++i)
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
        /// Lorsqu'un status du Handler a atteint sa fin, il est détruit
        /// </summary>
        private void OnStatusFinished(Status<T> status, float elapsedTime)
        {
            RemoveStatus(status);
        }
    }
}