using Sandbox;
using Sandbox.UI;

namespace ssl.Ui.NotificationHandler;

public class NotificationHandler : Panel
{
	private const float DEFAULT_DURATION = 5F;
	private Panel notification;
	private float remaining;


	private void RemoveNotification()
	{
		if ( notification == null )
		{
			return;
		}

		notification.Delete();
		notification = null;
	}

	public override void Tick()
	{
		base.Tick();

		if ( notification != null )
		{
			remaining -= Time.Delta;
			if ( remaining < 0 )
			{
				RemoveNotification();
			}
		}
	}


	[Event("ssl.notification")]
	public void DisplayNotification( string message )
	{
		int duration = 5;
		Log.Info("[NotificationHandler] Display notif " + message);
		RemoveNotification();
		notification = new Notification(message);
		AddChild(notification);
		remaining = duration;
	}
}
