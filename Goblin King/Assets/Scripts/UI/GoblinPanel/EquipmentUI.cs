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

        public void SetGoblin(GoblinStats goblinStats)
        {
            
        }

        public void SetItem(ItemSO itemSO)
        {
            itemImage.sprite = itemSO.sprite;
        }
        
    }
}