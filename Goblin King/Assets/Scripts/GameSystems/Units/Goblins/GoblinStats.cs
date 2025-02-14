using GameSystems.Items.SO;
using GameSystems.Storage;
using GameSystems.Units.Goblins.AI;
using Specific_Items;
using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public StorageData bag;
        private int maxEquipCount;
        [HideInInspector] public GoblinAI ai;

        public SpriteRenderer bagSprite;

        private void Awake()
        {
            ai = GetComponent<GoblinAI>();
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
                    bag = item.GetSpecificData<ItemBagData>().storage;
                    bagSprite.sprite = item.itemSO.sprite;
                    bagSprite.gameObject.SetActive(true);
                    itemSprite.gameObject.SetActive(false);
                } break;
                default:
                {
                    bag = null;
                    itemSprite.sprite = item.itemSO.sprite;
                    bagSprite.gameObject.SetActive(false);
                    itemSprite.gameObject.SetActive(true);

                    if (equippedItem.GetItemType() == ItemType.Weapon)
                    {
                        GetComponent<CombatAIBehaviour>().UpdateStats(equippedItem.GetSpecificData<ItemWeaponData>().weaponSO);
                    }
                } break;
            }
        }
        
        public bool HasTool(ToolType type)
        {
            return GetTool(type) != null;
        }

        public bool HasWeapon()
        {
            return equippedItem != null && equippedItem.GetItemType() == ItemType.Weapon;
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