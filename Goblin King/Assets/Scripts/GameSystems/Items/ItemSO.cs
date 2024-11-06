using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Items
{
    [CreateAssetMenu(fileName = "Item_", menuName = "SO/Items/Item", order = -500)]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        [HideInInspector] public ItemType itemType;
        [HideInInspector] public bool isEquipable;
        
        public enum ItemType
        {
            Weapon,
            Tool,
            Bag,
            Utility,
            
            BuildBlock,
            Resource
        }
    }
}
