using Specific_Items;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Bag", order = -502)]
    public class ItemSO_Bag : ItemSO
    {
        public ItemBagData itemBagData;
        public int slots;

        private void OnValidate()
        {
            itemType = ItemType.Bag;
            isEquipable = true;
        }
    }

    
    public class ItemBagData : ItemData
    {
        [HideInInspector] public Storage storage;
        public ItemSO_Bag bagSO;

        // Derived class constructor
        public ItemBagData(ItemSO_Bag bag) : base(bag) // Call base constructor
        {
            storage = new Storage(bag.slots);
            bagSO = bag;
        }
        public int GetSlots()
        {
            return bagSO.slots;
        }
        
    }
}