using System;
using GameSystems.Items.SO;
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

        public Image itemImage;

        public void SetItem(ItemData itemData)
        {
            //Remove current Item
            if (item != null && item != itemData)
            {
                OnRemoveItem.Invoke(item);
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
            
            OnAddItem.Invoke(item);
        }
        
    }
}