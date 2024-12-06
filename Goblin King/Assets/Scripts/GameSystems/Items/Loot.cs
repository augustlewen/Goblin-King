using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Interactions;
using GameSystems.Items.SO;
using GameSystems.Storage;
using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using Specific_Items;
using UnityEngine;

namespace GameSystems.Items
{
    public class Loot : TaskObject
    {
        private List<ItemData> items = new();
        
        public void SetLootTable(List<ItemData> loot)
        {
            items = loot;
            foreach (var itemData in items)
            {
                if(itemData.itemSO == null)
                    Debug.Log(itemData + "'s is null");
            }
            
            SelectTask();
        }

        public void LootItems(StorageData storage)
        {
            foreach (var itemData in items.ToList())
            {
                if (storage.AddItem(itemData))
                {
                    items.Remove(itemData);
                }
            }
            if(items.Count == 0)
                Destroy(gameObject);
        }
    }
}