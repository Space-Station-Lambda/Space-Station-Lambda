using System;
using System.Collections.Generic;
using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public class StatusHandler<T> where T : IEffectable
    {
        
        private List<Status<T>> statuses;

        public StatusHandler()
        {
            statuses = new List<Status<T>>();
        }
        
        /// <summary>
        /// Actualise tous les status
        /// </summary>
        /// <param name="affected">Entité affectée par les status</param>
        public void Update(T affected)
        {
            foreach (Status<T> status in statuses)
            {
                status.Update(affected);
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
            status.StatusStarted += OnStatusStart;
            status.StatusFinished += OnStatusFinished;
        }
        
        private void UnSubscribeEvent(Status<T> status)
        {
            status.StatusStarted -= OnStatusStart;
            status.StatusFinished -= OnStatusFinished;
        }

        /// <summary>
        /// Lorsqu'un status du Handler exécute sa première Update
        /// </summary>
        private void OnStatusStart(Status<T> status)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Lorsqu'un status du Handler a atteint sa fin, il est détruit
        /// </summary>

        private void OnStatusFinished(Status<T> status, StatusFinishedEventArgs e)
        {
            RemoveStatus(status);
        }
    }
}