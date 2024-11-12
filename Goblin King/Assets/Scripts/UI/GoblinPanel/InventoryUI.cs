using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class InventoryUI : MonoBehaviour
    {
        private GoblinStats stats;
        private Transform slotsLayout;

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
                if (stats.bag.items.Count > i)
                {
                    slot.SetActive(true);
                    slot.GetComponent<InventorySlotUI>().SetItem(stats.bag.items[i]);
                }
                else
                {
                    slot.SetActive(false);
                }
                
            }
        }
    }
}