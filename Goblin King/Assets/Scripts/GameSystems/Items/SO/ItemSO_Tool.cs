using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Tool", order = -501)]
    public class ItemSO_Tool : ItemSO
    {
        public ItemToolData itemToolData;
        public ToolType toolType;

        public int power;
        public float haste;
        
        private void OnValidate()
        {
            itemType = ItemType.Tool;
            isEquipable = true;
        }
        
    }
    
    public enum ToolType
    {
        Axe,
        Pickaxe,
        Sickle,
        Hoe
    }
    
    public class ItemToolData : ItemData
    {
        public ItemSO_Tool toolSO;
        
        public ItemToolData(ItemSO_Tool tool)
        {
            toolSO = tool;
            itemSO = tool;
        }
        
        
        
    }
    
}