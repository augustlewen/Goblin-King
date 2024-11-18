using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Interactions;
using GameSystems.Items.SO;
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
            OnSelectTask();
        }

        public void LootItems(BagInventory bagInventory)
        {
            foreach (var itemData in items.ToList())
            {
                if (bagInventory.AddItem(itemData))
                {
                    items.Remove(itemData);
                    Debug.Log("LOOTING : " + itemData.itemSO.itemName);
                }
            }
            Debug.Log("Items Left To Loot: " + items.Count);

            if(items.Count == 0)
                Destroy(gameObject);
        }
    }
}