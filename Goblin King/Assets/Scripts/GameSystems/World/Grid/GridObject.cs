using System;
using UnityEngine;

namespace GameSystems.World.Grid
{
    public class GridObject : MonoBehaviour
    {
        private string goName;
        private Vector2Int gridSize;
        
        public virtual void Setup(GridObjectSO gridObjectSO)
        {
            goName = gridObjectSO.gosoName;
            gridSize = gridObjectSO.gridSize;
            gameObject.name = gridObjectSO.gosoName;
            
            GetComponent<SpriteRenderer>().sprite = gridObjectSO.sprite;
        }

        private void OnDisable()
        {
            
        }
    }
}