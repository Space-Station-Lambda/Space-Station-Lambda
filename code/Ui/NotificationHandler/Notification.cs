using Sandbox.UI;

namespace ssl.Ui.NotificationHandler;

public class Notification : Panel
{
	public Notification( string text )
	{
		StyleSheet.Load("Ui/NotificationHandler/Notification.scss");
		Label label = new() {Text = text};
		AddChild(label);
	}
}
