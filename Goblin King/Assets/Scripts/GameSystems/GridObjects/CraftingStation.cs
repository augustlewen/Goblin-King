using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.GridObjects.SO;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Storage;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using UI.General;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class CraftingStation : TaskObject, ISelect
    {
        [HideInInspector]public List<Recipe> recipeList = new();
        [HideInInspector]public Recipe selectedRecipe;
        [HideInInspector]public int craftLimit = 0;

        [HideInInspector] public bool isCrafting;
        GoblinAI assignedGoblin;
        public const float craftingDuration = 4.0f;
        [HideInInspector] public float craftingProgress; 
        
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

        private void Update()
        {
            if(!isCrafting)
                return;

            craftingProgress += Time.deltaTime;
            if (craftingProgress >= craftingDuration)
            {
                craftingProgress = 0;
                CraftComplete();
            }

        }

        private void CraftComplete()
        {
            
        }

        public void UpdateSelectedRecipe(Recipe recipe)
        {
            if(recipe == selectedRecipe)
                return;
            
            selectedRecipe = recipe;
            craftLimit = 1;
            craftingProgress = 0;
            UpdateIsCrafting();
        }

        public void UpdateCraftLimit(int limit)
        {
            craftLimit = limit;
        }


        public void SelectObject()
        {
            WindowManager.i.OpenCraftingUI(this);
        }


        public void AssignGoblin(GoblinAI goblinAI)
        {
            assignedGoblin = goblinAI;
            UpdateIsCrafting();
        }


        private void UpdateIsCrafting()
        {
            isCrafting = assignedGoblin != null && StorageManager.HasItems(selectedRecipe.reagents);
        }
        
    }
}