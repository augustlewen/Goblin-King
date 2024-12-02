using System;
using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.Building
{
    public class PlacementObject : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Color viablePlacementColor = new Color(1, 1, 1, 0.8f);
        private Color nonviablePlacementColor = new Color(1, 0.4f, 0.4f, 0.8f);


        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetObject(GridObjectSO gridObjectSO)
        {
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
            WorldGrid.i.IsOccupied()
            //Set color based on buildable placement
            //If press, check if viable placement and then build
        }
        
        private void LateUpdate()
        {
            Vector3 position = Input.mousePosition;
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
        }
        
        private bool IsValidPosition(Vector2 position)
        {
            return !WorldGrid.i.IsOccupied(position);
        }
        
    }
}