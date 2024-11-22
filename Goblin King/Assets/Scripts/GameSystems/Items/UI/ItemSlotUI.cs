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

        [HideInInspector] public ItemData item;
        public TextMeshProUGUI countText;

        public Image itemImage;

        public int SetItem(ItemData itemData)
        {
            if (itemData == item)
            {
                Debug.Log("NOT MOVE ITEM");
                return 0;
            }
            
            //Set Slot To Empty
            if (itemData == null)
            {
                RemoveItem();
                return 0;
            }
            
            //If moving resource to another resource stack, make sure that all the counted resources can be added to stack
            if (item != null && item.GetItemType() != ItemType.Resource)
            {
                var resourceData = item.GetSpecificData<ItemResourceData>();
                if (item.itemSO == itemData.itemSO && resourceData.CanAddToStack())
                {
                    int remaining = resourceData.AddToStack(itemData.GetSpecificData<ItemResourceData>().count);
                    UpdateItemCount();

                    if (remaining == 0)
                        RemoveItem();
                        
                    return remaining;
                }
            }
            else if(item != null)
                RemoveItem();
            
            
            item = itemData;
            itemImage.sprite = itemData.GetSprite();
            itemImage.gameObject.SetActive(true);

            UpdateItemCount();
            
            OnAddItem.Invoke(item);
            return 0;
        }

        private void RemoveItem()
        {
            OnRemoveItem.Invoke(item);
            item = null;
            itemImage.gameObject.SetActive(false);
            UpdateItemCount();
        }
        
        private void UpdateItemCount()
        {
            if (item == null)
            {
                countText.text = "";
                return;
            }
            
            if (item.GetItemType() != ItemType.Resource)
                return;
            
            int count = item.GetSpecificData<ItemResourceData>().count;
            countText.text = count != 0 ? count.ToString() : "";
            
        }
        
    }
}