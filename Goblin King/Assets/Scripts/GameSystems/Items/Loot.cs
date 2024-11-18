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
            // for (int i = items.Count - 1; i >= 0; i--)
            // {
            //     var itemData = items[i];
            //     if (bagInventory.AddItem(itemData))
            //     {
            //         Debug.Log("LOOTING : " + items[i]);
            //         items.RemoveAt(i);
            //     }
            // }

            foreach (var itemData in items.ToList())
            {
                if (bagInventory.AddItem(itemData))
                {
                    items.Remove(itemData);
                    Debug.Log("LOOTING : " + itemData.itemName);
                }
            }
            Debug.Log("Items Left To Loot: " + items.Count);

            if(items.Count == 0)
                Destroy(gameObject);
        }
        
        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     GoblinStats goblinStats = other.GetComponent<GoblinStats>();
        //
        //     if (goblinStats != null && goblinStats.bag != null)
        //     {
        //         for (int i = items.Count - 1; i >= 0; i--)
        //         {
        //             var itemData = items[i];
        //             if (goblinStats.bag.AddItem(itemData))
        //             {
        //                 items.RemoveAt(i);
        //             }
        //         }
        //         
        //         if(items.Count == 0)
        //             Destroy(gameObject);
        //     }
        // }
    }
}