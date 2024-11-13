using System;
using UnityEngine;

namespace GameSystems.Items
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Bag", order = -502)]
    public class ItemSO_Bag : ItemSO
    {
        public int slots;
        
        private void OnValidate()
        {
            isEquipable = true;
            itemType = ItemType.Bag;
        }
    }
}