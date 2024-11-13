using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Items;
using GameSystems.Units.Goblins.AI;
using Specific_Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public ItemSO equippedItem;
        public BagInventory bag;
        private int maxEquipCount;
        [HideInInspector] public GoblinAI ai;

        public SpriteRenderer itemSprite;
        public SpriteRenderer bagSprite;

        private void Awake()
        {
            ai = GetComponent<GoblinAI>();
            
            if(equippedItem != null)
                EquipItem(equippedItem);
        }

        public void EquipItem(ItemSO item)
        {
            equippedItem = item;

            bool isBag = item.itemType == ItemSO.ItemType.Bag;
            bagSprite.gameObject.SetActive(isBag);
            itemSprite.gameObject.SetActive(!isBag);


            if (item is ItemSO_Bag itemBag)
            {
                bag = new BagInventory(itemBag.slots);
                bagSprite.sprite = item.sprite;
            }
            else if(item != null)
            {
                itemSprite.sprite = item.sprite;
            }
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