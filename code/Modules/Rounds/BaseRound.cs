using System;
using System.Collections.Generic;
using System.Globalization;
using Sandbox;
using ssl.Player;

namespace ssl.Modules.Rounds
{
    public abstract partial class BaseRound : NetworkComponent
    {
        public HashSet<MainPlayer> Players = new();
        public virtual int RoundDuration => 0;
        public virtual string RoundName => "";
        public float RoundEndTime { get; set; }
        public float TimeLeft => RoundEndTime - Time.Now;
        [Net] public string TimeLeftFormatted { get; set; }
        public event Action<BaseRound> RoundEndedEvent;

        public void Start()
        {
            if (Host.IsServer && RoundDuration > 0)
            {
                RoundEndTime = Time.Now + RoundDuration;
                TimeLeftFormatted = ((int)TimeLeft).ToString(CultureInfo.InvariantCulture);
                InitPlayers();
            }

            OnStart();
        }

        public void Stop()
        {
            Log.Info($"[Round] Round {this} stopped");
            if (Host.IsServer)
            {
                RoundEndTime = 0f;
                Players.Clear();
            }
        }

        /// <summary>
        /// When the round is finish
        /// </summary>
        public void Finish()
        {
            Log.Info($"[Round] Round {this} finished");
            OnFinish();
            RoundEndedEvent?.Invoke(this);
        }

        public void AddPlayer(MainPlayer player)
        {
            Log.Info($"[Round] Add player {player} to the round");
            Host.AssertServer();
            Players.Add(player);
        }

        public void RemovePlayer(MainPlayer player)
        {
            Log.Info($"[Round] Remove player {player} to the round");
            Host.AssertServer();
            Players.Remove(player);
        }

        public abstract BaseRound Next();

        public virtual void OnPlayerSpawn(MainPlayer player)
        {
            AddPlayer(player);
        }

        public virtual void OnPlayerKilled(MainPlayer player)
        {
            RemovePlayer(player);
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
                    TimeLeftFormatted = ((int)TimeLeft).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Default event when the round is finished.
        /// </summary>
        protected virtual void OnFinish()
        {
        }

        /// <summary>
        /// Default event when times is up.
        /// </summary>
        protected virtual void OnTimeUp()
        {
            Finish();
        }

        /// <summary>
        /// Set players to the list
        /// </summary>
        private void InitPlayers()
        {
            foreach (Client client in Client.All)
            {
                Players.Add((MainPlayer)client.Pawn);
            }
        }

        public override string ToString()
        {
            return $"Round Name: {RoundName}\n" +
                   $"Round Duration: {RoundDuration}\n" +
                   $"Round End: {RoundEndTime}({TimeLeftFormatted} left)";
        }
    }
}