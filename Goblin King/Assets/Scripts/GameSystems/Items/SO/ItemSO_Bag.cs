using Specific_Items;
using UnityEditor;
using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Bag", order = -502)]
    public class ItemSO_Bag : ItemSO
    {
        public ItemBagData itemBagData;
        private void OnValidate()
        {
            itemType = ItemType.Bag;
            isEquipable = true;
        }
    }

    [System.Serializable]
    public class ItemBagData : ItemData
    {
        public int slots;
        [HideInInspector] public BagInventory bagInventory;


        public ItemBagData(ItemSO_Bag bag)
        {
            slots = bag.itemBagData.slots;
            bagInventory = new BagInventory(slots);
            itemSO = bag;
        }
        
    }
}