using System.Collections.Generic;
using System.Linq;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class StorageObject : MonoBehaviour
    {
        private List<ItemData> storageItems = new();
        private int storageSize;

        public void Setup(int size)
        {
            storageSize = size;
        }

        public bool CanBePlacedInStorage(ItemData itemData)
        {
            if (storageItems.Count < storageSize)
                return true;

            return false;
        }
        
        public void AddToStorage(ItemData itemData)
        {
            foreach (var storageItem in storageItems.Where(storageItem => storageItem.itemSO == itemData.itemSO))
            {
                itemData.itemCount = storageItem.AddToStack(itemData.itemCount);
                if (itemData.itemCount == 0)
                    break;
            }

            if (itemData.itemCount > 0 && storageItems.Count < storageSize)
                storageItems.Add(itemData);
        }
    }
}