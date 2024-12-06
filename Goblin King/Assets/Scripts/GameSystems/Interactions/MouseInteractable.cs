using UnityEngine;

namespace GameSystems.Interactions
{
    public class MouseInteractable : MonoBehaviour
    {
        bool isHovering;
        private ISelect[] selectableComponents;

        private void Start()
        {
            selectableComponents = GetComponents<ISelect>();
        }

        private void Update()
        {
            if (isHovering && Input.GetMouseButtonDown(0))
                Select();
        }

        public void Select()
        {
            // Call OnSelect on each ISelect component
            foreach (var selectable in selectableComponents)
            {
                selectable.SelectObject();
            }
        }


        private void OnMouseEnter()
        {
            isHovering = true;
        }

        private void OnMouseExit()
        {
            isHovering = false;
        }
    }
}