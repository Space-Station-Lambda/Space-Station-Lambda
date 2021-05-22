using System;

namespace SSL_Core.pressure
{
    public class Simulation
    {
        private AtmosUnit[,] grid;

        public Simulation()
        {
            grid = new AtmosUnit[3, 3];
            for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i,j] = new();
            }
        }

        public void Randomize(int seed = 0)
        {
            Random random = new(seed);
            foreach (AtmosUnit atmosUnit in grid)
            {
                atmosUnit.Randomize(random.Next());
            }
        }

        public void Tick()
        {
            //Algo
            
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