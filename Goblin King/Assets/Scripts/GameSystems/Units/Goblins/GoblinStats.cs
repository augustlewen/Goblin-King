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
        public ItemSO startBagItem;
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
                ItemData itemData = startItem.CreateItemData();
                EquipItem(itemData);
            }
        }

        public void EquipItem(ItemData item)
        {
            equippedItem = item;

            if (item == null)
                return;
            
            switch (equippedItem.GetItemType())
            {
                case ItemType.Bag:
                {
                    bag = new BagInventory(item.GetSpecificData<ItemBagData>().GetSlots());
                    bag.AddItem(startBagItem.CreateItemData());
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
        
        public bool HasTool(ToolType type)
        {
            return GetTool(type) != null;
        }

        public ItemToolData GetTool(ToolType type)
        {
            // Check if item can be cast to ItemSO_Tool
            if (equippedItem is not ItemToolData toolItem) 
                return null;
            
            if (toolItem.toolSO.toolType != type)
                return null;
        
            return toolItem;
        }
    }
}