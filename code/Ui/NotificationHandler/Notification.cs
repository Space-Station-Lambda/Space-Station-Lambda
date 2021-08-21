using System;
using Sandbox;
using Sandbox.UI;

namespace ssl.Ui.NotificationHandler
{
    public class Notification : Panel
    {
        
        public Notification(string text)
        {
            StyleSheet.Load("Ui/NotificationHandler/Notification.scss");
            Label label = new Label
            {
                Text = text
            };
            AddChild(label);
        }
    }
}