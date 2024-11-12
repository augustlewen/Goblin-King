using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class GoblinPanelUI : MonoBehaviour
    {
        public Transform equipmentLayout;
        public Transform inventoryLayout;
        
        private void OnEnable()
        {
            UpdateUI();
        }


        private void UpdateUI()
        {
            DisableChildren(equipmentLayout);
            DisableChildren(inventoryLayout);
            
            for (int i = 0; i < GoblinManager.i.goblinParty.Count; i++)
            {
                var goblinStats = GoblinManager.i.goblinParty[i].stats;
                equipmentLayout.GetChild(i).GetComponent<EquipmentUI>().SetGoblin(goblinStats);

                if (goblinStats.bag != null)
                {
                    inventoryLayout.GetChild(i).GetComponent<InventoryUI>().SetGoblinStats(goblinStats);
                }
            }
            
        }

        private void DisableChildren(Transform parent)
        {
            foreach (Transform child in parent)
                child.gameObject.SetActive(false);
        }
    }
}