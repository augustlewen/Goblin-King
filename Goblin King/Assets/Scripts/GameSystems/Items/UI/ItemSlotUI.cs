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
        public void SetItem(ItemData itemSO)
        {
            if (item != null && item != itemSO)
            {
                Debug.Log("Remove Item");
                OnRemoveItem.Invoke(item);
            }
            
            item = itemSO;
            
            if (itemSO != null)
            {
                itemImage.sprite = itemSO.GetSprite();
                itemImage.gameObject.SetActive(true);
                
                OnAddItem.Invoke(item);
            }
            else
            {
                itemImage.gameObject.SetActive(false);
            }
        }
        
    }
}