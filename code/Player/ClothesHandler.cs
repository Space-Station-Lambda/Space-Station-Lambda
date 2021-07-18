using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

namespace ssl.Player
{
    /// <summary>
    /// Manage the clothing system for the given player.
    /// </summary>
    public class ClothesHandler
    {
        /// <summary>
        /// List of current clothes.
        /// </summary>
        private readonly List<ModelEntity> clothesList = new();

        /// <summary>
        /// Player concerned.
        /// </summary>
        private readonly MainPlayer player;

        public ClothesHandler(MainPlayer player)
        {
            this.player = player;
        }

        /// <summary>
        /// Attach a set of clothes to the player.
        /// </summary>
        /// <param name="clothesSet">Set of string clothes.</param>
        /// <param name="strip">If true, strip the player before add clothes.</param>
        public void AttachClothes(IEnumerable<string> clothesSet, bool strip = true)
        {
            if (strip) Strip();
            foreach (string c in clothesSet)
            {
                AttachClothes(c, false);
            }
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="clothesPart">Name of the clothes</param>
        /// <param name="strip">If true, strip the player before add clothes</param>
        public void AttachClothes(string clothes, bool strip = true)
        {
            AttachClothes(new Clothes(clothes), false);
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="clothesPart">Concerned clothes</param>
        /// <param name="strip">If true, strip the player before add clothes</param>
        public void AttachClothes(Clothes clothes, bool strip = true)
        {
            if (strip) Strip();
            clothes.SetParent(player, true);
            clothesList.Add(clothes);
        }

        /// <summary>
        /// Remove all clothes of the player
        /// </summary>
        public void Strip()
        {
            foreach (ModelEntity c in clothesList)
            {
                c.Delete();
            }

            clothesList.Clear();
        }
    }
}