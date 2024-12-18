using System;
using GameSystems.Units.Goblins;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager i;
        public Button toggleButton;

        private float targetPositionY;
        private float rectHeight;
        
        public void Awake()
        {
            i = this;
        }

        private void Start()
        {
            GoblinTaskManager.i.OnSpawnedGoblins.AddListener(UpdateInventoryUI);
            toggleButton.onClick.AddListener(ToggleSetPosition);
            rectHeight = GetComponent<RectTransform>().sizeDelta.y;
        }

        private void ToggleSetPosition()
        {
            targetPositionY = targetPositionY == 0 ? -rectHeight : 0;
        }

        private void Update()
        {
            if (Math.Abs(GetComponent<RectTransform>().anchoredPosition.y - targetPositionY) > 0.5f)
            {
                transform.position = new Vector3(transform.position.x,
                    Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * 30));
            }
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