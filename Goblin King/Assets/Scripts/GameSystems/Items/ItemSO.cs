using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Items
{
    [CreateAssetMenu(fileName = "Item_", menuName = "SO/ItemSo")]
    public class ItemSO : ScriptableObject
    {
        public string itemName;
        public bool isEquipable;
    }
}
