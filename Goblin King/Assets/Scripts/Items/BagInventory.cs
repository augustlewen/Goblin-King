using System.Collections.Generic;
using GameSystems.Items;
using UnityEngine;

namespace Items
{
    public class BagInventory
    {
        public List<ItemSO> items = new();
        private int slots;

        public BagInventory(int slotCount)
        {
            slots = slotCount;
        }
        
        public bool AddItem(ItemSO item)
        {
            if (items.Count < slots)
            {
                items.Add(item);
                return true;
            }

            return false;
        }

        private bool CanAddItem(ItemSO item)
        {
            return items.Count < slots;
        }
        
        
    }
}