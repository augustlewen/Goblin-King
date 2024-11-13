using GameSystems.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GoblinPanel
{
    public class ItemSlotUI : MonoBehaviour
    {
        public Image itemImage;

        public void SetItem(ItemSO itemSO)
        {
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