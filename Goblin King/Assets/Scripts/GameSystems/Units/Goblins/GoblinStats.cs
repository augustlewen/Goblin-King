using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Items;
using GameSystems.Units.Goblins.AI;
using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public List<ItemSO> equippedItems = new ();
        private int maxEquipCount;
        [HideInInspector] public GoblinAI ai;

        private void Awake()
        {
            ai = GetComponent<GoblinAI>();
        }

        public bool HasToolType(ItemSO_Tool.ToolType type)
        {
            return GetTool(type) != null;
        }

        public ItemSO_Tool GetTool(ItemSO_Tool.ToolType type)
        {
            foreach (var item in equippedItems)
            {
                // Check if item can be cast to ItemSO_Tool
                if (item is not ItemSO_Tool toolItem) 
                    continue;
                
                if (toolItem.toolType == type)
                    return toolItem;
            }
            return null;
        }
    }
}