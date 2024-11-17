using System;
using System.Collections.Generic;
using GameSystems.Items.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameSystems.Items
{
    [Serializable]
    public class DropTable
    {
        public ItemDrop[] itemDrops;
        
        [Serializable]
        public class ItemDrop
        {
            public ItemSO item;
            public float dropChance;
        }

        public void DropItems(Vector2 position)
        {
            var items = GetDroppedItems();

            foreach (var itemSO in items)
                ItemManager.i.DropNewItem(itemSO, position);
        }

        private ItemSO[] GetDroppedItems()
        {
            List<ItemSO> items = new();
            
            foreach (var itemDrop in itemDrops)
            {
                if (Random.Range(1, 101) <= itemDrop.dropChance)
                    items.Add(itemDrop.item);
            }

            return items.ToArray();
        }
    }
}