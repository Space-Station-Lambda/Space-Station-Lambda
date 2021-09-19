using Sandbox;
using ssl.Modules.Items.Data;
using ssl.Modules.Selection;
using ssl.Player;

namespace ssl.Modules.Items.Carriables
{
    public partial class ItemFood : Item<ItemFoodData>
    {
        private const string EatSound = "grunt1";
        
        public ItemFood()
        {
        }

        public ItemFood(ItemFoodData itemData) : base(itemData)
        {
        }

        /// <summary>
        /// First version, food feeds up the player on use
        /// </summary>
        public override void OnUsePrimary(MainPlayer player, ISelectable target)
        {
            PlayEatSound(player);
        }

        [ClientRpc]
        protected void PlayEatSound(Entity entity)
        {
            Sound.FromEntity(EatSound, entity);
        }
    }
}