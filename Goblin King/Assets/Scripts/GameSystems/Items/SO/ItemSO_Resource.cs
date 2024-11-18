using System;
using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Resource", order = -503)]
    public class ItemSO_Resource : ItemSO
    {
        public ItemResourceData itemResourceData;
        public int stackSize;
        
        private void OnValidate()
        {
            itemType = ItemType.Resource;
        }
        
        
    }
    
    public class ItemResourceData : ItemData
    {
        public ItemSO_Resource resourceSO;
        public int count;

        // Derived class constructor
        public ItemResourceData(ItemSO_Resource resource) : base(resource) // Call base constructor
        {
            resourceSO = resource;
        }

        public int AddToStack(int addCount)
        {
            count += addCount;
                
            if (count > resourceSO.stackSize)
            {
                int difference = count - resourceSO.stackSize;
                count = resourceSO.stackSize;
                return difference;
            }


            return 0;
        }

        public bool CanAddToStack()
        {
            return count < resourceSO.stackSize;
        }
        
    }
}