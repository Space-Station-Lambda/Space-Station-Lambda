using System.Collections.Generic;
using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public abstract partial class BaseRound : Networked
    {
        public event RoundEnd RoundEndedEvent;
        public virtual int RoundDuration => 0;
        public virtual string RoundName => "";
        public HashSet<MainPlayer> Players = new();
        public float RoundEndTime { get; set; }
        public float TimeLeft => RoundEndTime - Time.Now;

        public void Start()
        {
            if (Host.IsServer && RoundDuration > 0)
            {
                RoundEndTime = Time.Now + RoundDuration;
            }

            OnStart();
        }

        public void Finish()
        {
            if (Host.IsServer)
            {
                RoundEndTime = 0f;
                Players.Clear();
            }
            OnFinish();
        }

        public void AddPlayer(MainPlayer player)
        {
            Host.AssertServer();
            Players.Add(player);
        }
        public void RemovePlayer(MainPlayer player)
        {
            Host.AssertServer();
            Players.Remove(player);
        }

        public abstract BaseRound Next();

        public virtual void OnPlayerSpawn(MainPlayer player)
        {
        }

        public virtual void OnPlayerKilled(MainPlayer player)
        {
        }

        public virtual void OnPlayerLeave(MainPlayer player)
        {
        }

        public virtual void OnTick()
        {
        }

        public virtual void OnSecond()
        {
            if (Host.IsServer)
            {
                if (RoundEndTime > 0 && Time.Now >= RoundEndTime)
                {
                    RoundEndTime = 0f;
                    OnTimeUp();
                }
            }
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnFinish()
        {
        }

        protected virtual void OnTimeUp()
        {
            RoundEndedEvent?.Invoke(this);
        }
    }
}