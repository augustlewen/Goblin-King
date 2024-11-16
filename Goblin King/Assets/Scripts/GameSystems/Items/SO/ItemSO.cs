using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "Item_", menuName = "SO/Items/Item", order = -500)]
    public class ItemSO : ScriptableObject
    {
        [HideInInspector] public ItemType itemType;
        [HideInInspector] public bool isEquipable;
        public Sprite sprite;

        public ItemData GetItemData()
        {
            switch (itemType)
            {
                case ItemType.Bag : return ((ItemSO_Bag)this).itemBagData;
                case ItemType.Tool : return ((ItemSO_Tool)this).itemToolData;
            }
            
            return null;
        }
    }


    
    public class ItemData
    {
        public ItemSO itemSO;
        public string itemName;
        
        [HideInInspector] public int itemSOIndex;
        [HideInInspector] public int itemClassIndex;

        public Sprite GetSprite()
        {
            return itemSO.sprite;
        }
        
        public ItemType GetItemType()
        {
            return itemSO.itemType;
        }

        public bool IsEquipable()
        {
            return itemSO.isEquipable;
        }
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
