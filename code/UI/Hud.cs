using Sandbox;
using Sandbox.UI;

namespace SSL.UI
{
	public partial class Hud : HudEntity<RootPanel>
	{
		//TODO To config file
		private const string Path = "/UI/Hud.html";
		
		public Hud()
		{
			if (IsClient)
			{
				RootPanel.SetTemplate(Path);
			}
		}
	}
}
