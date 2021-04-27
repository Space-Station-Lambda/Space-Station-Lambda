using System;

namespace SSL_Core.model
{
    public class ItemData
    {
        public String Name { get;  }
        public int Weight { get;  }
        
        public int Max { get;  }

        public ItemData(String name, int weight, int max = 1)
        {
            Name = name;
            Weight = weight;
            Max = max;
        }
    }
}