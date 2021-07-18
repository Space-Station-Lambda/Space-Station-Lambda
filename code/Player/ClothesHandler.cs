using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
using ssl.Items.Data;

namespace ssl.Player
{
    /// <summary>
    /// Manage the clothing system for the given player.
    /// </summary>
    public class ClothesHandler
    {
        private readonly Dictionary<ClothesSlot, List<Clothes>> clothesDictionary = new();
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
                AttachClothes(c,ClothesSlot.None);
            }
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="clothes">Name of the clothes</param>
        /// <param name="slot">Slot concerned</param>
        public void AttachClothes(string clothes, ClothesSlot slot = ClothesSlot.None)
        {
            AttachClothes(new Clothes(clothes),slot);
        }

        /// <summary>
        /// Attach a piece of clothes to the player
        /// </summary>
        /// <param name="clothes">Concerned clothes</param>
        /// <param name="slot">Slot concerned</param>
        public void AttachClothes(Clothes clothes, ClothesSlot slot = ClothesSlot.None)
        {
            clothes.SetParent(player, true);
            //If the slot is not none, strip the slot
            if (slot != ClothesSlot.None)
            {
                Strip(slot);
            }

            if (!clothesDictionary.ContainsKey(slot))
            {
                clothesDictionary.Add(slot, new List<Clothes>());
            }
            clothesDictionary[slot].Add(clothes);
           
        }

        /// <summary>
        /// Remove all clothes of the player
        /// </summary>
        public void Strip()
        {
            foreach (List<Clothes> clothesList in clothesDictionary.Values)
            {
                foreach (Clothes clothes in clothesList)
                {
                    clothes.Delete();
                }
            }
            clothesDictionary.Clear();
        }
        
        public void Strip(ClothesSlot slot)
        {
            foreach ((ClothesSlot key, List<Clothes> value) in clothesDictionary.Where(keyValuePair => keyValuePair.Key == slot))
            {
                foreach (Clothes clothes in value)
                {
                    clothes.Delete();
                }
                clothesDictionary.Remove(key);
            }
        }
    }
}