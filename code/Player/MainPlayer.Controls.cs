using Sandbox;

namespace ssl.Player
{
    public partial class MainPlayer
    {
        private void CheckControls()
        {
            if (IsServer)
            {
                ServerControls();
            }

            if (IsClient)
            {
                ClientControls();
            }
        }
        
        private void ServerControls()
        {
            if (Input.Pressed(InputButton.Attack1))
            {
                SpawnCorpse();
            }
            if (Input.Pressed(InputButton.Reload))
            {
                Respawn();
            }
        }
        
        private void ClientControls()
        {
            
        }
    }
}