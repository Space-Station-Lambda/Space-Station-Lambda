using System;
using System.Collections.Generic;
using System.Linq;

namespace ssl.Modules
{
    /// <summary>
    /// A liquid is a set of different liquids. Liquid update is planned later in the developement. For now all liquids
    /// types are string and have a value. A Liquid can't have more than the max capacity. Liquid is kind of LiquidHandler.
    /// </summary>
    public class Liquid
    {
        
        public Liquid(string liquidName, int value) : this()
        {
            Add(liquidName, value);
        }
        
        public Liquid() : this(0)
        {
        }
        
        public Liquid(int capacity)
        {
            Capacity = capacity;
            Composition = new Dictionary<string, int>();
            InitComposition();
        }
        
        /// <summary>
        /// 0 Capacity mean infinit capacity
        /// </summary>
        public int Capacity { get; set; }

        public int CurrentAmountLiquids => Composition.Sum(l => l.Value);
        
        private Dictionary<string, int> Composition { get; }

        private void InitComposition()
        {
            Composition.Add("liquid.water", 0);
            Composition.Add("liquid.oil", 0);
            Composition.Add("liquid.biomass", 0);
            Composition.Add("liquid.fuel", 0);
            Composition.Add("liquid.waste", 0);
        }

        public void Add(string liquidName, int value)
        {
            if (Capacity != 0 && CurrentAmountLiquids + value > Capacity) throw new IndexOutOfRangeException();
            Composition[liquidName] += value;
        }
        
        /// <summary>
        /// Remove a liquid amount from this liquid 
        /// </summary>
        /// <param name="amount">The amount of liquid to remove</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Liquid ExtractLiquid(int amount)
        {
            /* TODO 
            Extract liquid with the ratio of each liquid
            */
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove a liquid amount from this liquid 
        /// </summary>
        /// <param name="liquidName">The liquid to remove</param>
        /// <param name="amount">The amount of liquid to remove</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Liquid ExtractLiquid(string liquidName, int amount)
        {
            if (amount > Composition[liquidName]) throw new ArgumentOutOfRangeException();
            Composition[liquidName] -= amount;
            return new Liquid(liquidName, amount);
        }

    }
}