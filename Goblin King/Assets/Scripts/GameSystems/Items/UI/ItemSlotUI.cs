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

        public void SetItem(ItemData itemData)
        {
            //Remove current Item
            if (item != null && item != itemData)
            {
                OnRemoveItem.Invoke(item);
                if (item.GetItemType() == ItemType.Resource)
                    UpdateItemCount(0);
            }
            
            //Set Slot To Empty
            if (itemData == null)
            {
                item = null;
                itemImage.gameObject.SetActive(false);
                return;
            }
            
            item = itemData;
            itemImage.sprite = itemData.GetSprite();
            itemImage.gameObject.SetActive(true);

            if (item.GetItemType() == ItemType.Resource)
                UpdateItemCount(item.GetSpecificData<ItemResourceData>().count);
            
            OnAddItem.Invoke(item);
        }

        private void UpdateItemCount(int count)
        {
            countText.text = count != 0 ? count.ToString() : "";
        }
        
    }
}