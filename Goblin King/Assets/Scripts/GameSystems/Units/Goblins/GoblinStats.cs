using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Items;
using GameSystems.Units.Goblins.AI;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public ItemSO equippedItem = new ();
        public BagInventory bag;
        private int maxEquipCount;
        [HideInInspector] public GoblinAI ai;

        private void Awake()
        {
            ai = GetComponent<GoblinAI>();
        }

        public void EquipItem(ItemSO item)
        {
            equippedItem = item;

            if (item is ItemSO_Bag itemBag)
                bag = new BagInventory(itemBag.slots);
        }
        
        public bool HasToolType(ItemSO_Tool.ToolType type)
        {
            return GetTool(type) != null;
        }

        public ItemSO_Tool GetTool(ItemSO_Tool.ToolType type)
        {
            // Check if item can be cast to ItemSO_Tool
            if (equippedItem is not ItemSO_Tool toolItem) 
                return null;
            
            if (toolItem.toolType != type)
                return null;
        
            return toolItem;
        }
    }
}