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
                ItemData itemData = ItemManager.CreateItemData(startItem);
                EquipItem(itemData);
            }
        }

        public void EquipItem(ItemData item)
        {
            if (item == null)
            {
                Debug.Log("Trying to equip invalid item. ItemData is NULL.");
                return;
            }
            
            equippedItem = item;

            switch (equippedItem.GetItemType())
            {
                case ItemType.Bag:
                {
                    bag = new BagInventory(ItemManager.GetBagData(item).slots);
                    bagSprite.sprite = item.GetSprite();
                    bagSprite.gameObject.SetActive(true);
                    itemSprite.gameObject.SetActive(false);
                } break;
                default:
                {
                    bag = null;
                    itemSprite.sprite = item.itemSO.sprite;
                    bagSprite.gameObject.SetActive(false);
                    itemSprite.gameObject.SetActive(true);
                } break;
            }
        }
        
        public bool HasTool(ItemToolData.ToolType type)
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