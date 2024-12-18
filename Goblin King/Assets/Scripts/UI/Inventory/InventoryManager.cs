using System;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace UI.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager i;
        public void Awake()
        {
            i = this;
        }

        private void Start()
        {
            GoblinTaskManager.i.OnSpawnedGoblins.AddListener(UpdateInventoryUI);
        }

        public Transform inventoryLayout;
        
        public void UpdateInventoryUI()
        {
            foreach (Transform child in inventoryLayout)
                child.gameObject.SetActive(false);
            
            for (int i = 0; i < GoblinTaskManager.i.goblinParty.Count; i++)
            {
                var goblinStats = GoblinTaskManager.i.goblinParty[i].stats;
                if (goblinStats.bag != null)
                    inventoryLayout.GetChild(i).GetComponent<InventoryUI>().SetGoblinStats(goblinStats);
            }
        }
        
    }
}