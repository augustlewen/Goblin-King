using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.InteractableObjects
{
    public class MouseInteractable : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnSelected = new ();
        bool isHovering;

        private void Update()
        {
            if (isHovering && Input.GetMouseButtonDown(0))
                Select();
        }

        protected virtual void Select()
        {
            OnSelected.Invoke();

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