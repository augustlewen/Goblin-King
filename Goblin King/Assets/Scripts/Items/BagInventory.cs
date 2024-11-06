using System.Collections.Generic;
using GameSystems.Items;

namespace Items
{
    public class BagInventory
    {
        public List<ItemSO> items;
        private int slots;

        public BagInventory(int slotCount)
        {
            slots = slotCount;
        }
        
        public void AddItem(ItemSO item)
        {
            if(items.Count < slots)
                items.Add(item);
        }
        
        
    }
}