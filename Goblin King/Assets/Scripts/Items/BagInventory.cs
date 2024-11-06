using System.Collections.Generic;
using GameSystems.Items;

namespace Items
{
    public class BagInventory
    {
        public List<ItemSO> items;
        public int slots;

        public BagInventory(int slotCount)
        {
            slots = slotCount;
        }
        
        
        
    }
}