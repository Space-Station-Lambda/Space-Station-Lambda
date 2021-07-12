using System;
using System.Collections.Generic;
using System.Globalization;
using Sandbox;
using ssl.Player;

namespace ssl.Rounds
{
    public abstract partial class BaseRound : NetworkComponent
    {
        public HashSet<MainPlayer> Players = new();
        public virtual int RoundDuration => 0;
        public virtual string RoundName => "";
        public float RoundEndTime { get; set; }
        public float TimeLeft => RoundEndTime - Time.Now;
        [Net] public string TimeLeftFormatted { get; set; }


        public void Start()
        {
            if (Host.IsServer && RoundDuration > 0)
            {
                RoundEndTime = Time.Now + RoundDuration;
                TimeLeftFormatted = TimeLeft.ToString(CultureInfo.InvariantCulture);
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
                else
                {
                    TimeLeftFormatted = TimeLeft.ToString(CultureInfo.InvariantCulture);
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
        }
    }
}