using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Items;
using GameSystems.Items.SO;
using GameSystems.Units.Goblins.AI;
using Specific_Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public ItemSO startItem;
        
        public ItemData equippedItem;
        public BagInventory bag;
        private int maxEquipCount;
        [HideInInspector] public GoblinAI ai;

        public SpriteRenderer itemSprite;
        public SpriteRenderer bagSprite;
        

        private void Awake()
        {
            ai = GetComponent<GoblinAI>();
            if (startItem != null)
            {
                ItemData itemData = startItem.GetItemData();
                // EquipItem();
            }
        }

        public void EquipItem(ItemData item)
        {
            equippedItem = item;

            bool isBag = item.itemType == ItemType.Bag;
            bagSprite.gameObject.SetActive(isBag);
            itemSprite.gameObject.SetActive(!isBag);


            if (item is ItemBagData itemBag)
            {
                bag = new BagInventory(itemBag.slots);
                bagSprite.sprite = item.sprite;
            }
            else if(item != null)
            {
                itemSprite.sprite = item.sprite;
            }
        }
        
        public bool HasToolType(ItemToolData.ToolType type)
        {
            return GetTool(type) != null;
        }

        public ItemToolData GetTool(ItemToolData.ToolType type)
        {
            // Check if item can be cast to ItemSO_Tool
            if (equippedItem is not ItemToolData toolItem) 
                return null;
            
            if (toolItem.toolType != type)
                return null;
        
            return toolItem;
        }
    }
}