using System.Collections.Generic;
using System.Linq;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;
using UnityEngine.Events;

namespace Specific_Items
{
    public class BagInventory
    {
        public UnityEvent<ItemData> onAddItem = new();
        public UnityEvent<ItemData> onUpdateItemStack = new();

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

            if (item.itemSO.itemType == ItemType.Resource)
            {
                var itemResourceData = GetStackableResourceData(item.itemSO);
                if (itemResourceData != null)
                {
                    itemResourceData.AddToStack(1);
                    onUpdateItemStack.Invoke(itemResourceData);
                    return true;
                }
            }
            onAddItem.Invoke(item);
            items.Add(item);
            return true;
        }

        private bool CanAddItem(ItemData item)
        {
            if (items.Count < slots)
                return true;

            if (item.itemSO.itemType == ItemType.Resource)
                return GetStackableResourceData(item.itemSO) != null;

            return false;
        }

        private ItemResourceData GetStackableResourceData(ItemSO itemSO)
        {
            foreach (var itemData in items)
            {
                if (itemData.itemSO.itemType == ItemType.Resource && itemData.itemSO == itemSO)
                {
                    if (itemData.GetSpecificData<ItemResourceData>().CanAddToStack())
                        return itemData.GetSpecificData<ItemResourceData>();
                }
            }

            return null;
        }
        
        
    }
}