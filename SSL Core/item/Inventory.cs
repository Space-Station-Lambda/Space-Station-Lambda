using System;

namespace SSL_Core.item
{
    public class Inventory
    {
        public ItemStack[] Items { get; }

        public int SlotsCount => Items.Length;

        public int SlotsLeft
        {
            get
            {
                int slotsCount = SlotsCount;
                int slotsLeft = 0;

                for (var i = 0; i < slotsCount; i++)
                {
                    if (Items[i] == null)
                    {
                        slotsLeft++;
                    }
                }

                return slotsLeft;
            }
        }
        
        private ItemAuthorizer authorizer;

        public Inventory(IItems items, int size)
        {
            Items = new Slot[size];
            authorizer = new ItemAuthorizer(items);
        }

        public void AddItem(ItemStack itemStack, int position = 0)
        {
            
        }
    }
}