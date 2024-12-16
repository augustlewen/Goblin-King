using GameSystems.GridObjects.SO;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.World.Grid
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/Breakable", order = 0)]
    public class GOSO_Breakable : GridObjectSO
    {
        public int hp;
        public ToolType breakTool;
        public LootTable lootTable;

        public Sprite childSprite;
    }
}