using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.RoundInfos;

public class RoundInfos : Panel
{
    private readonly List<RoundInfosPlayerLine> roundInfosPlayerLines = new();

    public RoundInfos()
    {
        StyleSheet.Load("Ui/RoundInfos/RoundInfos.scss");
        Log.Info("Register event...");
    }

    public override void Tick()
    {
        base.Tick();
        bool scorePressed = Input.Down(InputButton.Score);
        SetClass("hidden", !scorePressed);
        if (scorePressed) UpdatePlayers();
    }


    /// <summary>
    ///     Add player to the round info
    /// </summary>
    /// <param name="sslPlayer">The player to be added</param>
    private void AddPlayer(SslPlayer sslPlayer)
    {
        RoundInfosPlayerLine roudInfosPlayerLine = new(sslPlayer);
        AddChild(roudInfosPlayerLine);
        roundInfosPlayerLines.Add(roudInfosPlayerLine);
    }


    /// <summary>
    ///     Delete round info
    /// </summary>
    /// <param name="roundInfosPlayerLine">Round info to delete</param>
    private void RemoveRoundInfosPlayerLine(RoundInfosPlayerLine roundInfosPlayerLine)
    {
        roundInfosPlayerLine.Delete();
        roundInfosPlayerLines.Remove(roundInfosPlayerLine);
    }

    private void UpdatePlayers2() { }

    private List<SslPlayer> PlayersToAdd()
    {
        List<SslPlayer> players = new();
        foreach (Client client in Client.All)
        {
            SslPlayer sslPlayer = (SslPlayer) client.Pawn;
            bool founded = false;
            foreach (RoundInfosPlayerLine roundInfosPlayerLine in roundInfosPlayerLines)
            {
                if (roundInfosPlayerLine.SslPlayer.Equals(sslPlayer)) founded = true;
            }

            if (!founded) players.Add(sslPlayer);
        }

        return players;
    }

    private List<RoundInfosPlayerLine> RoundInfosPlayerLinesToRemove()
    {
        List<RoundInfosPlayerLine> roundInfosPlayerLines = new();
        foreach (RoundInfosPlayerLine roundInfosPlayerLine in this.roundInfosPlayerLines)
        {
            SslPlayer sslPlayer = roundInfosPlayerLine.SslPlayer;
            bool founded = false;
            foreach (Client client in Client.All)
            {
                if (((SslPlayer) client.Pawn).Equals(sslPlayer)) founded = true;

                break;
            }

            if (!founded) roundInfosPlayerLines.Add(roundInfosPlayerLine);
        }

        return roundInfosPlayerLines;
    }

    private void UpdatePlayers()
    {
        List<SslPlayer> playersToAdd = PlayersToAdd();
        List<RoundInfosPlayerLine> roundInfosPlayerLinesToRemove = RoundInfosPlayerLinesToRemove();
        foreach (SslPlayer mainPlayer in playersToAdd)
        {
            AddPlayer(mainPlayer);
        }

        foreach (RoundInfosPlayerLine roundInfosPlayerLine in roundInfosPlayerLinesToRemove)
        {
            RemoveRoundInfosPlayerLine(roundInfosPlayerLine);
        }
    }
}