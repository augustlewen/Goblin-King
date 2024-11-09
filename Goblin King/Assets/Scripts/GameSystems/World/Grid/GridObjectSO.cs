using UnityEngine;

namespace GameSystems.World.Grid
{
    [CreateAssetMenu(fileName = "Goso_", menuName = "SO/GridObject/GridObject", order = 0)]
    public class GridObjectSO : ScriptableObject
    {
        public string gosoName;
        public Vector2Int gridSize;
        public GridObjectType type;
        public Sprite sprite;

    }

    public enum GridObjectType
    {
        NonBreakable,
        Breakable,
        Station
    }
}