using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    class Inventory
    {
        public List<ItemHolder> ItemHolders = new List<ItemHolder>();
        public int filledSquares = 0;

        public Inventory(List<ItemHolder> itemHolders)
        {
            ItemHolders = itemHolders;
        }

        public void AddItem(Item item)
        {
            if (filledSquares == ItemHolders.Count)
            {

            }
            for (int i = 0; i < ItemHolders.Count; i++)
            {
                if(ItemHolders[i].item == null)
                {
                    ItemHolders[i].SetContainedItem(item);
                    break;
                }
            }
        }
        public void update(Knight knight)
        {
            for(int i = 0; i < ItemHolders.Count; i++)
            {
                ItemHolders[i].Clicked(knight);
            }
        }
    }
}
