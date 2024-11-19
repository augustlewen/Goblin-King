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
            //Remove current Item
            if (item != null && item != itemData)
            {
                if (item.GetItemType() != ItemType.Resource)
                    OnRemoveItem.Invoke(item);
                else
                {
                    var resourceData = item.GetSpecificData<ItemResourceData>();
                    if (item.itemSO == itemData.itemSO && resourceData.CanAddToStack())
                    {
                        int remaining = resourceData.AddToStack(itemData.GetSpecificData<ItemResourceData>().count);
                        UpdateItemCount(resourceData.count);
                        return remaining;
                    }
                }
                    
                
            }
            
            //Set Slot To Empty
            if (itemData == null)
            {
                item = null;
                itemImage.gameObject.SetActive(false);
                return 0;
            }
            
            item = itemData;
            itemImage.sprite = itemData.GetSprite();
            itemImage.gameObject.SetActive(true);

            if (item.GetItemType() == ItemType.Resource)
                UpdateItemCount(item.GetSpecificData<ItemResourceData>().count);
            
            OnAddItem.Invoke(item);
            return 0;
        }

        private void UpdateItemCount(int count)
        {
            countText.text = count != 0 ? count.ToString() : "";
        }
        
    }
}