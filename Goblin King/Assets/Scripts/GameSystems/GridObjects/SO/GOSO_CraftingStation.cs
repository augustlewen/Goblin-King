using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.World.Grid
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/CraftingStation", order = 0)]
    public class GOSO_CraftingStation : GOSO_Breakable
    {
        public ItemSO[] craftingItems;
    }
}