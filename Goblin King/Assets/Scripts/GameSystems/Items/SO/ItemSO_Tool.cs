using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Tool", order = -501)]
    public class ItemSO_Tool : ItemSO
    {
        public ItemToolData itemToolData;
        private void OnValidate()
        {
            isEquipable = true;
            itemType = ItemType.Tool;
            itemType = ItemType.Bag;
        }
        
    }
    
    [System.Serializable]
    public class ItemToolData : ItemData
    {
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

        public ItemToolData(ItemSO_Tool tool)
        {
            toolType = tool.itemToolData.toolType;
            power = tool.itemToolData.power;
            haste = tool.itemToolData.haste;
            itemSO = tool;
        }
        
    }
    
}