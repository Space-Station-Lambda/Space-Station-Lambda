using System;
using System.Collections.Generic;

namespace SSL_Core.systems.gas
{
    public class GasSimulation
    {
        private GasUnit[,] grid;
        private Dictionary<GasUnit, List<GasUnit>> neighbors;
        private IGasMovementStrategy gasMovementStrategy = new GasMovementBasicStrategy();
        
        public GasSimulation()
        {
            grid = new GasUnit[3, 3];
            neighbors = new();
            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i,j] = new();
            }
            GenerateNeighbors();
        }

        public void Randomize(int seed = 0)
        {
            Random random = new(seed);
            foreach (GasUnit gas in grid)
            {
                gas.Randomize(random.Next());
            }
        }

        public void GenerateNeighbors()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                neighbors.Add(grid[i,j], GetNeighbors(i, j));
            }
        }

        public void Tick(int nbr = 1)
        {
            for (int i = 0; i < nbr; i++)
            {
                ApplyGasMovements(GenerateGasMovements());
            }
        }

        private List<GasMovement> GenerateGasMovements()
        {
            List<GasMovement> movements = new();

            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                GasUnit gasUnit = grid[i, j];
                GasMovement movement = gasMovementStrategy.GenerateGasMovement(gasUnit, neighbors[gasUnit]);
                movements.Add(movement);
            }

            return movements;
        }

        private void ApplyGasMovements(List<GasMovement> gasMovements)
        {
            foreach (GasMovement gasMovement in gasMovements)
            {
                gasMovement.resolve();
            }
        }

        private List<GasUnit> GetNeighbors(int i, int j)
        {
            List<GasUnit> gasUnits = new();
            
            for (int k = i - 1; k <= i + 1; k++)
            for (int v = j - 1; v <= j + 1; v++)
            {
                if(k >= 0 && k < grid.GetLength(0) && v >= 0 && v < grid.GetLength(1) && !(k != i && v != j)) 
                    gasUnits.Add(grid[k,v]);
            }
            
            return gasUnits;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                str += "| ";
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    str += $"{grid[i,j]} | ";
                }
                str += "\n";
            }

            return str;
        }
    }
}