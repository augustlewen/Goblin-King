using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Items.UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        private ItemSO item;
        public Image itemImage;

        private void Awake()
        {
            
        }

        private void OnClick()
        {
            if(item != null)
                ItemInHand.HoldItem(item);
        }

        public void SetItem(ItemSO itemSO)
        {
            item = itemSO;
            
            if (itemSO != null)
            {
                itemImage.sprite = itemSO.sprite;
                itemImage.gameObject.SetActive(true);
            }
            else
            {
                itemImage.gameObject.SetActive(false);
            }
        }
    }
}