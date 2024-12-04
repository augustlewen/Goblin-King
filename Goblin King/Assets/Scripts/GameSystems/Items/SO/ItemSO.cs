using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "Item_", menuName = "SO/Items/Item", order = -500)]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        [HideInInspector] public ItemType itemType;
        [HideInInspector] public bool isEquipable;
        public Sprite sprite;

        public int stackSize;

        public ItemData CreateItemData()
        {
            switch (itemType)
            {
                case ItemType.Bag : var bag = new ItemBagData(this as ItemSO_Bag);
                    return bag;
                case ItemType.Tool : var tool = new ItemToolData(this as ItemSO_Tool);
                    return tool;
                case ItemType.Resource : var resource = new ItemResourceData(this as ItemSO_Resource);
                    return resource;
                case ItemType.Weapon :
                    var weapon = new ItemWeaponData(this as ItemSO_Weapon);
                    return weapon;
            }
        
            return null;
        }
        
    }


    
    public class ItemData
    {
        public ItemSO itemSO;
        public int itemCount = 1;
        
        public T GetSpecificData<T>() where T : class
        {
            return this as T;
        }
        public ItemType GetItemType() { return itemSO.itemType; }
        protected ItemData(ItemSO itemSO) { this.itemSO = itemSO; }

        public int AddToStack(int count)
        {
            if (itemSO.stackSize == 0 || itemCount == itemSO.stackSize)
                return count;

            itemCount += count;
            if (itemCount > itemSO.stackSize)
            {
                int remaining = itemCount - itemSO.stackSize;
                itemCount = itemSO.stackSize;
                return remaining;
            }
                
            return 0;
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
