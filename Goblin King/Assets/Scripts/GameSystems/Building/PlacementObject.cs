using System;
using GameSystems.GridObjects.SO;
using GameSystems.World;
using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Building
{
    public class PlacementObject : MonoBehaviour
    {
        [HideInInspector] public UnityEvent<Vector2> OnSelectPlacement = new();

        private SpriteRenderer spriteRenderer;
        private readonly Color viablePlacementColor = new (0.3f, 0.75f, 1, 0.8f);
        private readonly Color nonviablePlacementColor = new (1, 0.3f, 0.3f, 0.8f);

        private bool isBuilding;
        private Vector2 mousePosition;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetObject(GridObjectSO gridObjectSO)
        {
            isBuilding = gridObjectSO != null;
            
            if (gridObjectSO == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            gameObject.SetActive(true);
            spriteRenderer.sprite = gridObjectSO.sprite;
        }

        private void Update()
        {
            if(!isBuilding)
                return;
            
            if (IsValidPosition(mousePosition))
            {
                spriteRenderer.color = viablePlacementColor;
                if (Input.GetMouseButton(0))
                    OnSelectPlacement.Invoke(mousePosition);
            }
            else
            {
                spriteRenderer.color = nonviablePlacementColor;
            }
                
            //Set color based on buildable placement
            //If press, check if viable placement and then build
        }
        
        private void LateUpdate()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = WorldGrid.i.WorldToGridPosition(mousePosition);
            transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * 8f);
        }
        
        
        private static bool IsValidPosition(Vector2 position)
        {
            return !WorldGrid.i.IsOccupied(position) && BaseGenerator.IsPositionInBase(position);
        }
        
    }
}