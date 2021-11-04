﻿using ssl.Player;

namespace ssl.Modules.Rounds
{
    public partial class EndRound : BaseRound
    {
        public EndRound()
        {
        }

        public EndRound(RoundOutcome outcome)
        {
            RoundOutcome = outcome;
        }

        public RoundOutcome RoundOutcome { get; private set; }
        public override string RoundName => "Results round";
        public override int RoundDuration => 10;

        protected override void OnStart()
        {
            base.OnStart();
            foreach (MainPlayer mainPlayer in Players)
            {
                mainPlayer.Freeze();
            }
        }

        protected override void OnFinish()
        {
            base.OnFinish();
            foreach (MainPlayer mainPlayer in Players)
            {
                mainPlayer.Unfreeze();
            }
        }

        public override BaseRound Next()
        {
            return new PreRound();
        }
    }
}