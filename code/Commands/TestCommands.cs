﻿using Sandbox;
using ssl.Rounds;

namespace ssl.Commands
{
    public static class TestCommands
    {
        [AdminCmd]
        public static void ping()
        {
            Log.Info("Pong");
        }
        
        [AdminCmd]
        public static void preround()
        {
            Gamemode.Instance.RoundManager.ChangeRound(new PreRound());
        }

        [AdminCmd]
        public static void round()
        {
            Gamemode.Instance.RoundManager.ChangeRound(new InProgressRound());
        }
        
    }
}