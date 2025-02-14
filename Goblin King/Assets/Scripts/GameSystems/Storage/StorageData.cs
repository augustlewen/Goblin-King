using System.Collections.Generic;
using System.Linq;
using GameSystems.Items.SO;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Storage
{
    public class StorageData
    {
        public UnityEvent OnUpdateStorage = new();

        public List<ItemData> storageItems = new();
        [HideInInspector] public int slots;

        public StorageData(int slotCount)
        {
            slots = slotCount;
        }
        
        public bool CanAddItem(ItemData itemData)
        {
            if (storageItems.Count < slots)
                return true;

            return false;
        }
        
        public bool AddItem(ItemData newItem)
        {
            foreach (var storageItem in storageItems.Where(storageItem => storageItem.itemSO == newItem.itemSO))
            {
                newItem.itemCount = storageItem.AddToStack(newItem);
                OnUpdateStorage.Invoke();
                if (newItem.itemCount == 0)
                    return true;
            }

            if (newItem.itemCount > 0 && storageItems.Count < slots)
            {
                storageItems.Add(newItem);
                OnUpdateStorage.Invoke();
                return true;
            }
            
            return false;
        }

        public void RemoveItem(ItemData newItem)
        {
            storageItems.Remove(newItem);
            OnUpdateStorage.Invoke();
        }


        public int GetItemCount(ItemSO item)
        {
            int itemCount = 0;
            foreach (var itemData in storageItems)
            {
                if (itemData.itemSO = item)
                    itemCount += itemData.itemCount;
            }

            return itemCount;
        }
        
    }
}