using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.GridObjects.SO
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/Storage", order = 0)]
    public class GOSO_Storage : GOSO_Breakable
    {
        public int size;
    }
}