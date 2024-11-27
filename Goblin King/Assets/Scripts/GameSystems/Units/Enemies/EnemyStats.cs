using System;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.Units.Enemies
{
    public class EnemyStats : UnitStats
    {
        public ItemSO startItem;
        public LootTable lootTable;
        
        private void Start()
        {
            if(startItem != null)
                EquipItem(startItem.CreateItemData());
        }

        protected override void OnDeath()
        {
            lootTable.DropItems(transform.position);
            base.OnDeath();
        }

        public void EquipItem(ItemData item)
        {
            equippedItem = item;

            if (item == null)
                return;

            if (equippedItem.GetItemType() != ItemType.Weapon) 
                return;
            
            var weapon = item.GetSpecificData<ItemWeaponData>().weaponSO;
            GetComponent<CombatAIBehaviour>().UpdateStats(weapon.power, weapon.range, weapon.haste);
        }

    }
}