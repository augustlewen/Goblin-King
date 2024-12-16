using System;
using System.Collections.Generic;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.World.Grid;
using UI.General;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class CraftingStation : MonoBehaviour, ISelect
    {
        [HideInInspector]public List<Recipe> recipeList = new();
        [HideInInspector]public Recipe selectedRecipe;
        [HideInInspector]public int craftLimit = 0;
        
        private void Awake()
        {
            gameObject.AddComponent<MouseInteractable>();
        }

        public void Setup(GridObjectSO gridObjectSO)
        {
            var gosoCrafting = gridObjectSO as GOSO_CraftingStation;
            
            foreach (var item in gosoCrafting!.craftingItems)
                recipeList.Add(item.craftingRecipe);

            selectedRecipe = recipeList[0];
        }

        public void UpdateSelectedRecipe(Recipe recipe)
        {
            if(recipe == selectedRecipe)
                return;
            
            selectedRecipe = recipe;
            craftLimit = 1;
        }

        public void UpdateCraftLimit(int limit)
        {
            craftLimit = limit;
        }


        public void SelectObject()
        {
            WindowManager.i.OpenCraftingUI(this);
        }
    }
}