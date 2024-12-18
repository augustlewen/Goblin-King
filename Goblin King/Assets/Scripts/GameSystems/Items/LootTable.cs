using System;
using System.Collections.Generic;
using GameSystems.Items.SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameSystems.Items
{
    [Serializable]
    public class LootTable
    {
        public ItemDrop[] itemDrops;
        
        [Serializable]
        public class ItemDrop
        {
            public ItemSO item;
            public int amount = 1;
            public float dropChance;
        }
        
        public void DropItems(Vector2 position)
        {
            var items = GetDroppedItems();
            if(items.Length == 0)
                return;
            
            List<ItemData> loot = new();
            foreach (var itemSO in items)
            {
                var item = itemSO.CreateItemData();
                loot.Add(item);
            }
            
            ItemManager.i.DropItems(loot, position);
        }

        private ItemSO[] GetDroppedItems()
        {
            List<ItemSO> items = new();

            foreach (var itemDrop in itemDrops)
            {
                if (!(Random.Range(1, 101) <= itemDrop.dropChance)) 
                    continue;
                
                for(int i = 0; i < itemDrop.amount; i++)
                    items.Add(itemDrop.item);
                
                if(itemDrop.amount == 0)
                    items.Add(itemDrop.item);
            }

            return items.ToArray();
        }
    }
}