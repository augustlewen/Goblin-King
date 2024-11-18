using System;
using GameSystems.Items.UI;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class InventoryUI : MonoBehaviour
    {
        private GoblinStats stats;
        public Transform slotsLayout;

        private void OnEnable()
        {
            UpdateUI();
        }

        public void SetGoblinStats(GoblinStats goblinStats)
        {
            stats = goblinStats;
            gameObject.SetActive(true);
        }

        private void UpdateUI()
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                
                slot.SetActive(stats.bag.slots > i);
                
                Debug.Log(stats.bag.items.Count);
                
                if (stats.bag.items.Count > i)
                    slot.GetComponent<ItemSlotUI>().SetItem(stats.bag.items[i]);
                
            }
        }
    }
}