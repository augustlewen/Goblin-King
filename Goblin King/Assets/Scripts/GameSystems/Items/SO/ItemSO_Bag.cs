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
        [HideInInspector] public BagInventory bagInventory;
        public ItemSO_Bag bagSO;

        public ItemBagData(ItemSO_Bag bag)
        {
            bagInventory = new BagInventory(bag.slots);
            bagSO = bag;
            itemSO = bag;
        }

        public int GetSlots()
        {
            return bagSO.slots;
        }
        
    }
}