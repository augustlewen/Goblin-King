using GameSystems.Items;
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

        public void SetGoblin(GoblinStats goblinStats)
        {
            stats = goblinStats;
            gameObject.SetActive(true);

            SetItem(stats.equippedItem);
        }

        public void SetItem(ItemSO itemSO)
        {
            itemImage.gameObject.SetActive(itemSO != null);
            
            if(itemSO == null)
                return;
            itemImage.sprite = itemSO.sprite;
        }
        
    }
}