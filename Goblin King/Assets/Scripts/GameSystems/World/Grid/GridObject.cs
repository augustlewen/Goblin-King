using System;
using UnityEngine;

namespace GameSystems.World.Grid
{
    public class GridObject : MonoBehaviour
    {
        private string goName;
        private Vector2Int gridSize;
        
        private SpriteRenderer sr;
        
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public virtual void Setup(GridObjectSO gridObjectSO)
        {
            goName = gridObjectSO.gosoName;
            gridSize = gridObjectSO.gridSize;
            
            sr.sprite = gridObjectSO.sprite;
        }

        private void OnDisable()
        {
            
        }
    }
}