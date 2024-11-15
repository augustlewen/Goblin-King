using System.Collections.Generic;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace Specific_Items
{
    public class BagInventory
    {
        public List<ItemData> items = new();
        [HideInInspector] public int slots;

        public BagInventory(int slotCount)
        {
            slots = slotCount;
        }
        
        public bool AddItem(ItemData item)
        {
            if (items.Count < slots)
            {
                items.Add(item);
                return true;
            }

            return false;
        }

        private bool CanAddItem(ItemData item)
        {
            return items.Count < slots;
        }
        
        
    }
}