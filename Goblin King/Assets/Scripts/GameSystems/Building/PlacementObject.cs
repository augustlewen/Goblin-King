using System;
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
        private Color viablePlacementColor = new Color(1, 1, 1, 0.8f);
        private Color nonviablePlacementColor = new Color(1, 0.4f, 0.4f, 0.8f);

        private bool isBuilding;

        private void Start()
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
            
            spriteRenderer.sprite = gridObjectSO.sprite;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if(!isBuilding)
                return;
            
            if (IsValidPosition(Input.mousePosition))
            {
                spriteRenderer.color = viablePlacementColor;
                if (Input.GetMouseButton(0))
                    OnSelectPlacement.Invoke(Input.mousePosition);
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
            Vector3 position = Input.mousePosition;
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
        }
        
        
        private static bool IsValidPosition(Vector2 position)
        {
            return !WorldGrid.i.IsOccupied(position) && !BaseGenerator.IsPositionInBase(position);
        }
        
    }
}