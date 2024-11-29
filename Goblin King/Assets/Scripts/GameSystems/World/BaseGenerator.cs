using System;
using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.World
{
    public class BaseGenerator : MonoBehaviour
    {
        private Vector2Int size = new (22, 16);
        private Vector2Int center = new (0, 0);

        private static int minX;
        private static int maxX;
        private static int minY;
        private static int maxY;

        private void Awake()
        {
            maxX = (size.x / 2) + center.x;
            minX = (-size.x / 2) + center.x;
            maxY = (size.y / 2) + center.y;
            minY = (-size.y / 2) + center.y;
        }

        public static bool IsPositionInBase(Vector2 position)
        {
            var gridPos = WorldGrid.i.WorldToGridPosition(position);
            return gridPos.x > minX && gridPos.x < maxX && gridPos.y > minY && gridPos.y < maxY;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(minX, minY, 1), new Vector3(maxX, minY, 1));
            Gizmos.DrawLine(new Vector3(minX, minY, 1), new Vector3(minX, maxY, 1));
            Gizmos.DrawLine(new Vector3(maxX, minY, 1), new Vector3(maxX, maxY, 1));
            Gizmos.DrawLine(new Vector3(minX, maxY, 1), new Vector3(maxX, maxY, 1));


        }
    }
}