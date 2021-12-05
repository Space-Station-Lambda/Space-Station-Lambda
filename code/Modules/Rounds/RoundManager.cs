using Sandbox;

namespace ssl.Modules.Rounds;

public partial class RoundManager : BaseNetworkable
{
	public RoundManager()
	{
		if ( Host.IsServer )
		{
			ChangeRound(new PreRound());
		}
	}

	//public event Action RoundStarted;

	[Net] public BaseRound CurrentRound { get; private set; }

	public void ChangeRound( BaseRound round )
	{
		Host.AssertServer();

		if ( CurrentRound != null )
		{
			CurrentRound.Stop();
			CurrentRound.RoundEndedEvent -= OnRoundEnd;
			Log.Info("Round " + CurrentRound.RoundName + " ended");
		}

		CurrentRound = round;
		CurrentRound.Start();
		CurrentRound.RoundEndedEvent += OnRoundEnd;
		//RoundStarted?.Invoke();
		Log.Info("Round " + CurrentRound.RoundName + " started");
	}

	private void OnRoundEnd( BaseRound round )
	{
		ChangeRound(round.Next());
	}
}
