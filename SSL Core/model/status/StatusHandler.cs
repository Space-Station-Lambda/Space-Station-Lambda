using System;
using System.Collections.Generic;
using SSL_Core.model.interfaces;
using SSL_Core.model.player;

namespace SSL_Core.model.status
{
    public class StatusHandler<T> : List< Status<T> > where T : IEffectable
    {
        /// <summary>
        /// Actualise tous les status du Handler et les supprime s'ils sont finis
        /// </summary>
        /// <param name="affected">Entité affectée par les status</param>
        public void Update(T affected)
        {
            foreach (Status<T> status in this)
            {
                status.Update(affected);
                
                if ( status.UpdateTick() )
                {
                    Remove(status);
                }
            }
        }
    }
}