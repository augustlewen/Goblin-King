using GameSystems.Items;
using UnityEngine;

namespace GameSystems.World.Grid
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/Breakable", order = 0)]
    public class GOSO_Breakable : GridObjectSO
    {
        public int hp;
        public ItemSO_Tool.ToolType breakTool;
        
    }
}