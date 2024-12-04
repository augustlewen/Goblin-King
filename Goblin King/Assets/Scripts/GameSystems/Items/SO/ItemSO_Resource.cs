using System;
using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Resource", order = -503)]
    public class ItemSO_Resource : ItemSO
    {
        public ItemResourceData itemResourceData;
        
        private void OnValidate()
        {
            itemType = ItemType.Resource;
        }
        
        
    }
    
    public class ItemResourceData : ItemData
    {
        public ItemSO_Resource resourceSO;

        // Derived class constructor
        public ItemResourceData(ItemSO_Resource resource) : base(resource) // Call base constructor
        {
            resourceSO = resource;
        }
    }
}