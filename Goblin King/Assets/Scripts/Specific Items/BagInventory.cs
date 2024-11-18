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
            if (!CanAddItem(item))
                return false;
            
            if (items.Count < slots)
            {
                items.Add(item);
                return true;
            }

            return false;
        }

        private bool CanAddItem(ItemData item)
        {
            if (items.Count < slots)
                return true;

            foreach (var itemData in items)
            {
                if (itemData.GetItemType() == ItemType.Resource)
                {
                    
                }
            }

            return false;
        }
        
        
    }
}