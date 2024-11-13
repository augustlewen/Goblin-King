using GameSystems.Items;
using GameSystems.Items.UI;
using GameSystems.Units.Goblins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GoblinPanel
{
    public class EquipmentUI : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public Image goblinImage;
        public Image itemImage;

        private GoblinStats stats;
        public ItemSlotUI itemSlot;

        public void SetGoblin(GoblinStats goblinStats)
        {
            stats = goblinStats;
            gameObject.SetActive(true);

            itemSlot.SetItem(stats.equippedItem);
        }

        public void SetItem(ItemSO itemSO)
        {
            itemSlot.SetItem(itemSO);
        }
        
    }
}