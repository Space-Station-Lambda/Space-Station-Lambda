using Sandbox;
using Sandbox.UI;
using ssl.Player;

namespace ssl.Ui.PlayerHUD;

public class RoleIndicator : Panel
{
    private Label roleLabel;
    
    public RoleIndicator()
    {
        StyleSheet.Load("Ui/PlayerHUD/RoleIndicator.scss");
        roleLabel = AddChild<Label>();
    }

    public override void Tick()
    {
        base.Tick();
        if (((SslPlayer) Local.Pawn).RoleHandler.Role is not null)
            roleLabel.Text = ((SslPlayer) Local.Pawn).RoleHandler.Role.Name;
        
        SetClass("hidden", false);
    }
}