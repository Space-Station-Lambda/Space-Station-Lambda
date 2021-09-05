﻿using System.Collections.Generic;
using System.Linq;
using ssl.Player;

namespace ssl.Modules.Clothes
{
    /// <summary>
    /// Manage the clothing system for the given player.
    /// </summary>
    public class ClothesHandler
    {
        private readonly List<Clothes> clothes = new();

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
                AttachClothes(c);
            }
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="slot">Slot concerned</param>
        /// <param name="clothesModel">Model of the clothes</param>
        public void AttachClothes(string clothesModel)
        {
            AttachClothes(new Clothes(clothesModel));
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="clothes">Concerned clothes</param>
        /// <param name="slot">Slot concerned</param>
        public void AttachClothes(Clothes pieceOfClothes)
        {
            pieceOfClothes.SetParent(player, true);
            clothes.Add(pieceOfClothes);
        }

        /// <summary>
        /// Remove all clothes of the player
        /// </summary>
        public void Strip()
        {
            foreach (Clothes pieceOfClothes in clothes)
            {
                pieceOfClothes.Delete();
            }
            clothes.Clear();
        }
    }
}