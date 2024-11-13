using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.GoblinPanel
{
    public class GoblinPanelUI : MonoBehaviour
    {
        public static GoblinPanelUI i;
        
        public Transform equipmentLayout;
        public Transform inventoryLayout;

        private void Awake()
        {
            i = this;
        }

        private void OnEnable()
        {
            UpdateUI();
        }


        public void UpdateUI()
        {
            DisableChildren(equipmentLayout);
            DisableChildren(inventoryLayout);
            
            for (int i = 0; i < GoblinManager.i.goblinParty.Count; i++)
            {
                var goblinStats = GoblinManager.i.goblinParty[i].stats;
                equipmentLayout.GetChild(i).GetComponent<EquipmentUI>().SetGoblin(goblinStats);

                Debug.Log(goblinStats.bag);
                
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