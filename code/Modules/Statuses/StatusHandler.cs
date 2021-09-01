﻿using System.Collections.Generic;
using System.Linq;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Statuses
{
    public partial class StatusHandler : NetworkedEntityAlwaysTransmitted
    {
        [Net] private MainPlayer player { get; set; }
        
        public StatusHandler()
        {
        }
        
        public StatusHandler(MainPlayer player)
        {
            Statuses = new List<Status>();
            this.player = player;
        }
        
        [Net] public List<Status> Statuses { get; private set; }

        /// <summary>
        /// Add status. Create a new one if he is not affected by the status
        /// </summary>
        /// <param name="status">The status to add</param>
        public void ApplyStatus(Status status)
        {
            foreach (Status s in Statuses.Where(s => !s.IsInfinite).Where(s => s.Id.Equals(status.Id)))
            {
                s.TimeLeft += status.TimeLeft;
                return;
            }
            Statuses.Add(status);
            status.OnApply(player);
        }

        public void ResolveStatus(Status status)
        {
            foreach (Status s in Statuses.Where(s => status.Id.Equals(s.Id)))
            {
                if (!s.IsInfinite) s.TimeLeft -= status.TimeLeft;
                if (s.TimeLeft <= 0 || s.IsInfinite)
                {
                    s.OnResolve(player);
                    Statuses.Remove(s);
                    return;
                }
            }
        }

        /// <summary>
        /// Tick statuses and remove status if ended timer
        /// </summary>
        public void Tick()
        {
            HashSet<Status> statusesToTick = new(Statuses);
            foreach (Status status in statusesToTick)
            {
                status.OnTick(player);
                if (status.TimeLeft <= 0 && !status.IsInfinite)
                {
                    status.OnEnd(player);
                    Statuses.Remove(status);
                }
            }
        }
    }
}