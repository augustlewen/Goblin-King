using System.Collections.Generic;
using GameSystems.Items.SO;
using GameSystems.Items.UI;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        private GoblinStats stats;
        public Transform slotsLayout;

        private void Awake()
        {
            UpdateUI();
        }


        public void SetGoblinStats(GoblinStats goblinStats)
        {
            stats = goblinStats;
            stats.bag.OnUpdateStorage.AddListener(UpdateUI);
            gameObject.SetActive(true);
        }

       

        private void UpdateUI()
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                slot.SetActive(stats.bag.slots > i);
                
                if (stats.bag.storageItems.Count > i)
                    slot.GetComponent<ItemSlotUI>().SetItem(stats.bag.storageItems[i]);
            }
        }

        private void AddItemToUI(ItemData item)
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                var itemSlot = slot.GetComponent<ItemSlotUI>();

                if (itemSlot.slotItem == null)
                    itemSlot.SetItem(item);
            }
        }
        
    }
}