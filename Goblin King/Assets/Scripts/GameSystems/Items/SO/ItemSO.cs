using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent OnUpdateCount = new();

        public ItemSO itemSO;
        public int itemCount = 1;
        
        public T GetSpecificData<T>() where T : class
        {
            return this as T;
        }
        public ItemType GetItemType() { return itemSO.itemType; }
        protected ItemData(ItemSO itemSO) { this.itemSO = itemSO; }


        // public void SetItem(ItemData newItem)
        // {
        //     if (this == newItem)
        //         return;
        //
        //     if (newItem == null)
        //     {
        //         OnRemoveItem.Invoke();
        //         return;
        //     }
        //     
        //     
        //     // If moving resource to another resource stack, make sure that all the counted resources can be added to stack
        //     if (itemSO == newItem.itemSO)
        //     {
        //         int remaining = AddToStack(newItem);
        //
        //         if(remaining == 0)
        //             newItem.OnRemoveItem.Invoke();
        //         
        //         OnUpdateCount.Invoke();
        //         newItem.OnUpdateCount.Invoke();
        //         return;
        //     }
        //     
        //     
        //     if(slotItem != null)
        //         RemoveItem();
        //     
        //     //Replace the old item with the new item
        //     
        //     itemImage.sprite = newItemData.itemSO.sprite;
        //     itemImage.gameObject.SetActive(true);
        //
        //     UpdateItemCount();
        //     
        //     OnAddItem.Invoke(slotItem);
        //     return 0;
        // }
        
        public int AddToStack(ItemData otherItem)
        {
            if (itemSO.stackSize == 0 || itemCount == itemSO.stackSize)
                return otherItem.itemCount;

            itemCount += otherItem.itemCount;
            otherItem.itemCount = 0;
            
            if (itemCount > itemSO.stackSize)
            {
                int remaining = itemCount - itemSO.stackSize;
                itemCount = itemSO.stackSize;
                
                otherItem.itemCount = remaining;
                otherItem.OnUpdateCount.Invoke();
                OnUpdateCount.Invoke();
                return remaining;
            }
            
            OnUpdateCount.Invoke();
            otherItem.OnUpdateCount.Invoke();
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
