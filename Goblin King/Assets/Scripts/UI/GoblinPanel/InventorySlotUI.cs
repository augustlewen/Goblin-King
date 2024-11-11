using GameSystems.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GoblinPanel
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image itemImage;

        public void SetItem(ItemSO itemSO)
        {
            itemImage.sprite = itemSO.sprite;
        }
    }
}