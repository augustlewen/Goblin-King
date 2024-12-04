using GameSystems.Items;
using UnityEngine;

namespace GameSystems.World.Grid
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/GridObject", order = 10)]
    public class GridObjectSO : ScriptableObject
    {
        public string gosoName;
        public Vector2Int gridSize;
        public GridObjectType type;
        public Sprite sprite;

        public Recipe recipe;
    }

    public enum GridObjectType
    {
        NonBreakable,
        Breakable,
        Station,
        Storage
    }
}