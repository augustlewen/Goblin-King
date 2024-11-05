using UnityEngine;

namespace GameSystems.Items
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Tool", order = -501)]
    public class ItemSO_Tool : ItemSO
    {
        private void OnValidate()
        {
            isEquipable = true;
            itemType = ItemType.Tool;
        }

        public ToolType toolType;
        public int power;
        public float haste;
        
        public enum ToolType
        {
            Axe,
            Pickaxe,
            Sickle,
            Hoe
        }
        
    }
}