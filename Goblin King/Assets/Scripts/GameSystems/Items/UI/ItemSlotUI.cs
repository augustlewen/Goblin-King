using System;
using GameSystems.Items.SO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystems.Items.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<ItemData> OnAddItem = new ();
        [HideInInspector] public UnityEvent<ItemData> OnRemoveItem = new ();

        [HideInInspector] public ItemData slotItem;
        public TextMeshProUGUI countText;

        public Image itemImage;

        public int SetItem(ItemData newItemData)
        {
            if (newItemData == slotItem)
            {
                Debug.Log("NOT MOVE ITEM");
                return 0;
            }
            
            //Set Slot To Empty
            if (newItemData == null)
            {
                RemoveItem();
                return 0;
            }

            // If moving resource to another resource stack, make sure that all the counted resources can be added to stack
            if (slotItem != null && slotItem.itemSO == newItemData.itemSO)
            {
                int remaining = slotItem.AddToStack(newItemData.itemCount);
                UpdateItemCount();
                
                if(remaining == 0)
                    RemoveItem();
                
                return remaining;
            }
            if(slotItem != null)
                RemoveItem();
            
            //Replace the old item with the new item
            slotItem = newItemData;
            itemImage.sprite = newItemData.itemSO.sprite;
            itemImage.gameObject.SetActive(true);

            UpdateItemCount();
            
            OnAddItem.Invoke(slotItem);
            return 0;
        }

        private void RemoveItem()
        {
            OnRemoveItem.Invoke(slotItem);
            slotItem = null;
            itemImage.gameObject.SetActive(false);
            UpdateItemCount();
        }
        
        private void UpdateItemCount()
        {
            if (slotItem == null)
            {
                countText.text = "";
                return;
            }
            
            if (slotItem.GetItemType() != ItemType.Resource)
                return;
            
            int count = slotItem.GetSpecificData<ItemResourceData>().count;
            countText.text = count != 0 ? count.ToString() : "";
            
        }
        
    }
}