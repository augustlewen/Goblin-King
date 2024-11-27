using UnityEngine;

namespace GameSystems.Items.SO
{
    [CreateAssetMenu(fileName = "It_", menuName = "SO/Items/Weapon", order = -504)]
    public class ItemSO_Weapon : ItemSO
    {
        private ItemWeaponData itemWeaponData;

        public int power;
        public float haste;
        public float range;
        public GameObject projectile;
        
        private void OnValidate()
        {
            itemType = ItemType.Weapon;
            isEquipable = true;
        }
        
    }
    
    
    public class ItemWeaponData : ItemData
    {
        public ItemSO_Weapon weaponSO;
        // Derived class constructor
        public ItemWeaponData(ItemSO_Weapon weapon) : base(weapon) // Call base constructor
        {
            weaponSO = weapon;
        }
        
    }
}