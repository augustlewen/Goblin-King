using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "Item_", menuName = "SO/Items/Item", order = -500)]
    public class ItemSO : ScriptableObject
    {
        [HideInInspector] public ItemType itemType;

        public ItemData GetItemData()
        {
            switch (itemType)
            {
                case ItemType.Bag : return ((ItemSO_Bag)this).itemBagData;
                break;
                case ItemType.Tool : return ((ItemSO_Tool)this).itemToolData;
                break;
            }
            
            return null;
        }
    }


    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        [HideInInspector] public ItemType itemType;
        [HideInInspector] public bool isEquipable;
        public Sprite sprite;
    }
    
    public enum ItemType
    {
        Weapon,
        Tool,
        Bag,
        Utility,
            
        BuildBlock,
        Resource
    }
}
