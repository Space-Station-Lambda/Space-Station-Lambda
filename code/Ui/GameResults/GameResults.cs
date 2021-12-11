using System;
using Sandbox.UI;
using Sandbox.UI.Construct;
using ssl.Modules.Rounds;

namespace ssl.Ui.GameResults;

public class GameResults : Panel
{
	private const string TIE_TEXT = "Tie";
	private const string PROTAGONISTS_TEXT = "The Lambda Company wins !";
	private const string TRAITORS_TEXT = "The Traitors wins !";

	private readonly Label roundOutcome;

	public GameResults()
	{
		StyleSheet.Load("Ui/GameResults/GameResults.scss");

		roundOutcome = Add.Label(classname: "winning-team");

		SetClass("active", false);
		SetClass("hidden", true);
	}

	public override void Tick()
	{
		base.Tick();

		EndRound currentRound = Gamemode.Instance.RoundManager?.CurrentRound as EndRound;

		SetClass("active", null != currentRound);
		SetClass("hidden", null == currentRound);

		if ( null == currentRound )
		{
			return;
		}

		switch ( currentRound.RoundOutcome )
		{
			case RoundOutcome.Tie:
				roundOutcome.SetText(TIE_TEXT);
				break;
			case RoundOutcome.ProtagonistsWin:
				roundOutcome.SetText(PROTAGONISTS_TEXT);
				break;
			case RoundOutcome.TraitorsWin:
				roundOutcome.SetText(TRAITORS_TEXT);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
