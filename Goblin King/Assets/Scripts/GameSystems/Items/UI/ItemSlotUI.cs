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

        // ReSharper disable Unity.PerformanceAnalysis
        public void SetItem(ItemData itemData)
        {
            if (itemData == null)
            {
                itemImage.gameObject.SetActive(false);
                return;
            }
            
            if (item != null && item != itemData)
            {
                OnRemoveItem.Invoke(item);
            }
            
            item = itemData;
            
            itemImage.sprite = itemData.GetSprite();
            itemImage.gameObject.SetActive(true);
            
            OnAddItem.Invoke(item);
            
        }
        
    }
}