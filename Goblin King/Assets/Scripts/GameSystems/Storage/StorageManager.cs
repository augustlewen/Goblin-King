using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.GridObjects;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Storage
{
    public class StorageManager : MonoBehaviour
    {
        // private UnityEvent<ItemData> OnAddItem = new();
        // private UnityEvent<ItemData> OnRemoveItem = new();

        private static StorageManager i;
        // public List<StorageObject> storageObjects = new();
        private StorageData storageData;
        
        private void Awake()
        {
            i = this;
            storageData = new(0);
        }

        // public static void AddStorageData(StorageObject storageObject)
        // {
        //     i.storageObjects.Add(storageObject);
        // }

        public static void AddStorage(int size)
        {
            i.storageData.slots += size;
        }
        
        // public static UnityEvent<ItemData> GetOnAddItemEvent()
        // {
        //     return i.OnAddItem;
        // }
        //
        // public static UnityEvent<ItemData> GetOnRemoveItemEvent()
        // {
        //     return i.OnRemoveItem;
        // }

        public static List<ItemData> GetStorageItems()
        {
            // List<ItemData> items = new();
            //
            // foreach (var storageObject in i.storageObjects)
            //     items.AddRange(storageObject.storage.storageItems);

            return i.storageData.storageItems;
        }

        public static StorageData GetStorageData()
        {
            return i.storageData;
        }

        public static bool HasItems(ItemObject[] itemList)
        {
            return itemList.All(Item => HasItem(Item));
        }
        
        public static bool HasItem(ItemObject item)
        {
            int count = item.count;
            foreach (var storageItem in GetStorageData().storageItems)
            {
                if (storageItem.itemSO != item.itemSO) 
                    continue;
                
                count -= storageItem.itemCount;

                if (count <= 0)
                    return true;
            }

            return false;
        }
    }
}