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
                status.UpdateTime();
                status.Update(affected);
            }
        }
        
        public void AddStatus(Status<T> status)
        {
            status.StatusFinished += OnStatusFinished;
            
            statuses.Add(status);
        }

        public void RemoveStatus(Status<T> status)
        {
            statuses.Remove(status);
        }

        /// <summary>
        /// Lorsqu'un status du Handler exécute sa première Update
        /// </summary>
        private void OnStatusStart(object sender, EventArgs e)
        {
            // NotImplemented
        }
        
        /// <summary>
        /// Lorsqu'un status du Handler a atteint sa fin, il est détruit
        /// </summary>

        private void OnStatusFinished(object sender, StatusFinishedEventArgs e)
        {
            if (sender is Status<T>)
            {
                Status<T> status = sender as Status<T>;
                
                status.StatusFinished -= OnStatusFinished;
                RemoveStatus(status);
            }
        }
    }
}