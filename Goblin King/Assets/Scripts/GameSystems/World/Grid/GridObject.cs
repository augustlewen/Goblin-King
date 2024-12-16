using System;
using GameSystems.GridObjects;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.World.Grid
{
    public class GridObject : MonoBehaviour
    {
        // public UnityEvent<GridObjectSO> OnGridObjectSetup = new();
        
        private string goName;
        private Vector2Int gridSize;
        private SpriteRenderer spriteRenderer;

        public GridObjectSO gridObjectSO;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Setup(GridObjectSO goso)
        {
            goName = goso.gosoName;
            gridSize = goso.gridSize;
            gameObject.name = goso.gosoName;
            spriteRenderer.sprite = goso.sprite;

            gridObjectSO = goso;

            switch (gridObjectSO.type)
            {
                case GridObjectType.Breakable : gameObject.AddComponent<BreakableObject>().Setup(gridObjectSO);
                    break;
                case GridObjectType.Station : 
                    // gameObject.AddComponent<BreakableObject>().Setup(gridObjectSO);
                    gameObject.AddComponent<CraftingStation>().Setup(gridObjectSO);
                    break;
                case GridObjectType.Storage : gameObject.AddComponent<StorageObject>().Setup(gridObjectSO);
                    break;
            }
            
            // OnGridObjectSetup.Invoke(goso);
        }

        private void OnDisable()
        {
            
        }
    }
}