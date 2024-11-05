using System.Collections.Generic;
using GameSystems.Items;
using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class GoblinStats : UnitStats
    {
        public List<ItemSO> equippedItems = new ();
        private int maxEquipCount;
        
    }
}