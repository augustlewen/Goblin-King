using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.InteractableObjects
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

        private void Select()
        {
            // Call OnSelect on each ISelect component
            foreach (var selectable in selectableComponents)
            {
                selectable.OnSelect();
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