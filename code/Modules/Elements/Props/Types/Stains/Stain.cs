using Sandbox;
using ssl.Modules.Elements.Props.Data.Stains;
using ssl.Player;

namespace ssl.Modules.Elements.Props.Types.Stains
{
    public class Stain : Prop<StainData>
    {
        /// <summary>
        /// Health of a basic stain
        /// </summary>
        private int health = 10;
        
        public Stain()
        {
        }

        public Stain(StainData itemData) : base(itemData)
        {
            CollisionGroup = CollisionGroup.Trigger;
        }

        public void CleanStain(int strength)
        {
            health -= strength;
            if (health < 0)
            {
                Delete();
            }
        }
        
        public override void OnInteract(Player.SslPlayer sslPlayer)
        {
            //TODO Debug purpose
            Log.Info(health);
        }
    }
}