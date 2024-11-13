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
            
            itemSlot.OnAddItem.AddListener(OnAddItem);
            itemSlot.OnRemoveItem.AddListener(OnRemoveItem);

        }

        private void OnRemoveItem(ItemSO item)
        {
            stats.equippedItem = null;
            if (item.itemType == ItemSO.ItemType.Bag)
                stats.bag = null;
            
        }


        private void OnAddItem(ItemSO item)
        {
            stats.equippedItem = item;
        }

        public void SetItem(ItemSO itemSO)
        {
            itemSlot.SetItem(itemSO);
        }
        
    }
}