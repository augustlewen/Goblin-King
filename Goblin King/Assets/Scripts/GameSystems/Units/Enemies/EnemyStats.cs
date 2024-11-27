using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.Units.Enemies
{
    public class EnemyStats : UnitStats
    {
        public ItemSO startItem;
        public LootTable lootTable;
        public Transform targetArrow;
        
        private void Start()
        {
            if(startItem != null)
                EquipItem(startItem.CreateItemData());
            
            GetComponent<TaskObject>().OnSelectTask.AddListener(OnSelectTask);
        }

        private void OnSelectTask()
        {
            targetArrow.gameObject.SetActive(true);
        }

        protected override void OnDeath()
        {
            lootTable.DropItems(transform.position);
            targetArrow.gameObject.SetActive(false);
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
            GetComponent<CombatAIBehaviour>().UpdateStats(weapon);
        }

    }
}