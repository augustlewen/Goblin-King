using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class InventoryUI : MonoBehaviour
    {
        private GoblinStats goblinStats;
        private Transform slotsLayout;
        
        private void UpdateUI()
        {
            for (int i = 0; i < slotsLayout.childCount; i++)
            {
                var slot = slotsLayout.GetChild(i).gameObject;
                if (goblinStats.bag.items.Count > i)
                {
                    slot.SetActive(true);
                    slot.GetComponent<InventorySlotUI>().SetItem(goblinStats.bag.items[i]);
                }
                else
                {
                    slot.SetActive(false);
                }
                
            }
        }
    }
}