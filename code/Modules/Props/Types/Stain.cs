using Sandbox;
using ssl.Modules.Items.Carriables;
using ssl.Modules.Props.Data;
using ssl.Player;

namespace ssl.Modules.Props.Types
{
    public class Stain : Prop<PropData>
    {
        /// <summary>
        /// Health of a basic stain
        /// </summary>
        private int health = 10;
        
        public Stain()
        {
        }

        public Stain(PropData itemData) : base(itemData)
        {
        }

        public void CleanStain(int strength)
        {
            health -= strength;
            if (health < 0)
            {
                Delete();
            }
        }
        
        public override void OnInteract(MainPlayer player)
        {
            //TODO Debug purpose
            Log.Info(health);
        }
    }
}