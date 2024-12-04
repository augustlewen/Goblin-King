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

        public void SetItem(ItemData newItemData)
        {
            if (newItemData == slotItem)
                return;
            
            //Set Slot To Empty
            if (newItemData == null)
            {
                RemoveItem();
                return;
            }

            // If moving resource to another resource stack, make sure that all the counted resources can be added to stack
            if (slotItem != null)
            {
                if (slotItem.itemSO == newItemData.itemSO)
                {
                    slotItem.AddToStack(newItemData);
                    return;
                }
                OnRemoveItem.Invoke(slotItem);
            }
            
            //Replace the old item with the new item
            slotItem = newItemData;
            slotItem.OnUpdateCount.AddListener(UpdateItemCount);
            itemImage.sprite = newItemData.itemSO.sprite;
            itemImage.gameObject.SetActive(true);

            UpdateItemCount();
            OnAddItem.Invoke(slotItem);
        }
        
        
        // public int SetItem(ItemData newItemData)
        // {
        //     if (newItemData == slotItem)
        //         return 1;
        //     
        //     //Set Slot To Empty
        //     if (newItemData == null)
        //     {
        //         RemoveItem();
        //         return 0;
        //     }
        //
        //     // If moving resource to another resource stack, make sure that all the counted resources can be added to stack
        //     if (slotItem != null)
        //     {
        //         if (slotItem.itemSO == newItemData.itemSO)
        //         {
        //             int remaining = slotItem.AddToStack(newItemData);
        //             return remaining;
        //         }
        //         OnRemoveItem.Invoke(slotItem);
        //     }
        //     
        //     //Replace the old item with the new item
        //     slotItem = newItemData;
        //     slotItem.OnUpdateCount.AddListener(UpdateItemCount);
        //     itemImage.sprite = newItemData.itemSO.sprite;
        //     itemImage.gameObject.SetActive(true);
        //
        //     UpdateItemCount();
        //     OnAddItem.Invoke(slotItem);
        //     return 0;
        // }


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
            
            int count = slotItem.itemCount;
            countText.text = count != 0 ? count.ToString() : "";
            
        }
        
    }
}