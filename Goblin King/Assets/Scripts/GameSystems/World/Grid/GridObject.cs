using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.World.Grid
{
    public class GridObject : MonoBehaviour
    {
        public UnityEvent<GridObjectSO> OnGridObjectSetup = new();
        
        private string goName;
        private Vector2Int gridSize;
        
        public void Setup(GridObjectSO gridObjectSO)
        {
            goName = gridObjectSO.gosoName;
            gridSize = gridObjectSO.gridSize;
            gameObject.name = gridObjectSO.gosoName;
            
            GetComponent<SpriteRenderer>().sprite = gridObjectSO.sprite;
            
            OnGridObjectSetup.Invoke(gridObjectSO);
        }

        private void OnDisable()
        {
            
        }
    }
}