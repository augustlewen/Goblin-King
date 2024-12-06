using System;
using System.Collections.Generic;
using GameSystems.GridObjects;
using GameSystems.Items.SO;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Storage
{
    public class StorageManager : MonoBehaviour
    {
        private UnityEvent<ItemData> OnAddItem = new();
        private UnityEvent<ItemData> OnRemoveItem = new();

        private static StorageManager i;
        public List<StorageObject> storageObjects = new();
        
        private void Awake()
        {
            i = this;
        }

        public static void AddStorageData(StorageObject storageObject)
        {
            i.storageObjects.Add(storageObject);
        }

        public static UnityEvent<ItemData> GetOnAddItemEvent()
        {
            return i.OnAddItem;
        }
        
        public static UnityEvent<ItemData> GetOnRemoveItemEvent()
        {
            return i.OnRemoveItem;
        }
        
    }
}