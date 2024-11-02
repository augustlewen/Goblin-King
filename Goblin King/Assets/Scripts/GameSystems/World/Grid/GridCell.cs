using System.Collections.Generic;
using UnityEngine;

namespace GameSystems.World.Grid
{
    public class GridCell
    {
        public List<GameObject> objectsInCell;

        public GridCell()
        {
            objectsInCell = new List<GameObject>();
        }

        public void AddObject(GameObject obj)
        {
            objectsInCell.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            objectsInCell.Remove(obj);
        }
    }
}