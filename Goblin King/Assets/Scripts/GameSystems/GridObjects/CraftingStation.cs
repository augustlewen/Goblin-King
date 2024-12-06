using GameSystems.Interactions;
using GameSystems.Items;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class CraftingStation : MonoBehaviour, ISelect
    {
        private Recipe recipe;
        
        private void Awake()
        {
            gameObject.AddComponent<MouseInteractable>();
        }


        public void SelectObject()
        {
            
        }
    }
}