using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace ssl.Player
{
    public class ClothesHandler
    {
        private readonly List<ModelEntity> clothes = new();
        private readonly MainPlayer player;

        public ClothesHandler(MainPlayer player)
        {
            this.player = player;
        }

        public void AttachClothes(IEnumerable<string> clothesSet, bool strip = true)
        {
            if (strip) Strip();
            foreach (Clothes clothesPart in clothesSet.Select(c => new Clothes(c)))
            {
                clothesPart.SetParent(player, true);
                clothes.Add(clothesPart);
            }
        }

        /// <summary>
        /// Remove all clothes of the player
        /// </summary>
        public void Strip()
        {
            foreach (ModelEntity c in clothes)
            {
                c.Delete();
            }

            clothes.Clear();
        }
    }
}