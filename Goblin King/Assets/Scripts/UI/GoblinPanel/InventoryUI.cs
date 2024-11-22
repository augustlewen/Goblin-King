using System;
using GameSystems.Items.SO;
using GameSystems.Items.UI;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class InventoryUI : MonoBehaviour
    {
        private GoblinStats stats;
        public Transform slotsLayout;

        private void Awake()
        {
            SetupUI();
        }


        public void SetGoblinStats(GoblinStats goblinStats)
        {
            stats = goblinStats;
            gameObject.SetActive(true);
        }

        private void SetupUI()
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                slot.SetActive(stats.bag.slots > i);
                
                if (stats.bag.items.Count > i)
                    slot.GetComponent<ItemSlotUI>().SetItem(stats.bag.items[i]);
            }
        }

        private void AddItemToUI(ItemData item)
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                var itemSlot = slot.GetComponent<ItemSlotUI>();

                if (itemSlot.item == null)
                    itemSlot.SetItem(item);
            }
        }
        
    }
}