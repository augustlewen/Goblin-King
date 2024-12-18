using System;
using GameSystems.Items.SO;
using GameSystems.Items.UI;
using UnityEngine;

namespace GameSystems.Storage
{
    public class StorageUI : MonoBehaviour
    {
        public Transform contentLayout;

        private void Awake()
        {
            foreach (Transform child in contentLayout)
            {
                child.GetComponent<ItemSlotUI>().OnAddItem.AddListener(OnAddItem);
                child.GetComponent<ItemSlotUI>().OnRemoveItem.AddListener(OnRemoveItem);
            }
        }

        private void OnAddItem(ItemData itemData)
        {
            StorageManager.GetStorageData().AddItem(itemData);
        }
        private void OnRemoveItem(ItemData itemData)
        {
            StorageManager.GetStorageData().RemoveItem(itemData);
        }
        
        
        private void OnEnable()
        {
            for (int i = 0; i < contentLayout.childCount; i++)
            {
                var slot = contentLayout.GetChild(i).GetComponent<ItemSlotUI>();
                var storageItems = StorageManager.GetStorageItems();
                var storageSlots = StorageManager.GetStorageData().slots;
                
                slot.gameObject.SetActive(i < storageSlots);
                if (i >= storageItems.Count)
                    continue;

                slot.slotItem = null;
                slot.SetItem(storageItems[i]);
            }
        }
        
        
        
    }
}