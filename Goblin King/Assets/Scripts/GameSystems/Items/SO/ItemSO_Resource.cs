using System;
using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Resource", order = -503)]
    public class ItemSO_Resource : ItemSO
    {
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

        public ItemResourceData(ItemSO_Resource resource)
        {
            resourceSO = resource;
            itemSO = resource;
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
        
    }
}