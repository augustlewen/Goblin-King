using GameSystems.Interactions;
using UnityEngine;

namespace ObjectSelection
{
    public class DragSelect : MonoBehaviour
    {
        public RectTransform selectionBox;
        private Vector2 startMousePosition;
        private Vector2 endMousePosition;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Start the drag process
                startMousePosition = Input.mousePosition;
                selectionBox.gameObject.SetActive(true);
            }

            if (Input.GetMouseButton(0))
            {
                // Update the end position and draw the selection box
                endMousePosition = Input.mousePosition;
                UpdateSelectionBox();
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Finalize selection
                SelectObjectsInBox();
                selectionBox.gameObject.SetActive(false);
            }
        }

        private void UpdateSelectionBox()
        {
            Vector2 boxStart = startMousePosition;
            Vector2 boxSize = endMousePosition - startMousePosition;

            // Adjust for negative values (dragging in the opposite direction)
            if (boxSize.x < 0)
            {
                boxStart.x += boxSize.x;
                boxSize.x = -boxSize.x;
            }
            if (boxSize.y < 0)
            {
                boxStart.y += boxSize.y;
                boxSize.y = -boxSize.y;
            }

            // Set the selection box position and size
            selectionBox.anchoredPosition = boxStart;
            selectionBox.sizeDelta = boxSize;
        }

        private void SelectObjectsInBox()
        {
            // Convert screen-space corners to world-space corners
            Vector2 worldStart = mainCamera.ScreenToWorldPoint(startMousePosition);
            Vector2 worldEnd = mainCamera.ScreenToWorldPoint(endMousePosition);

            // Ensure worldStart is bottom-left and worldEnd is top-right for OverlapArea
            Vector2 bottomLeft = new Vector2(Mathf.Min(worldStart.x, worldEnd.x), Mathf.Min(worldStart.y, worldEnd.y));
            Vector2 topRight = new Vector2(Mathf.Max(worldStart.x, worldEnd.x), Mathf.Max(worldStart.y, worldEnd.y));

            // Calculate the center and size of the overlap box
            Vector3 center = (bottomLeft + topRight) / 2;
            Vector3 size = new Vector3(Mathf.Abs(topRight.x - bottomLeft.x), Mathf.Abs(topRight.y - bottomLeft.y), 1);

            // Use Physics.OverlapBox to find all colliders within the selection box
            Collider[] colliders = Physics.OverlapBox(center, size / 2, Quaternion.identity); // size / 2 because OverlapBox uses half-extents

            foreach (var collider in colliders)
            {
                // Check if the collider’s game object has a MouseInteractable component
                var interactable = collider.GetComponent<MouseInteractable>();
                if (interactable != null)
                {
                    interactable.Select();  // Call Select() on interactable objects
                }
            }
        }
    }

}
