using System;
using GameSystems.GridObjects;
using GameSystems.Items;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameSystems.Crafting
{
    public class CraftingUI : MonoBehaviour
    {
        public TMP_Dropdown dropdown;
        public Image[] reagentImages;
        public Image craftedItemImage;

        private CraftingStation craftingStation;

        [Header("Craft Limit")] 
        public TextMeshProUGUI limitText;
        public Button limitIncreaseButton;
        public Button limitDecreaseButton;

        private void Awake()
        {
            dropdown.onValueChanged.AddListener(OnDropDownChanged);
            limitIncreaseButton.onClick.AddListener(IncreaseLimit);
            limitDecreaseButton.onClick.AddListener(DecreaseLimit);

        }
        private void IncreaseLimit() { UpdateLimit(1); }
        private void DecreaseLimit() { UpdateLimit(-1); }

        private void UpdateLimit(int change)
        {
            int currentLimit = craftingStation.craftLimit;

            currentLimit += change;

            limitDecreaseButton.interactable = currentLimit > 0;
            limitIncreaseButton.interactable = currentLimit < 99;
            limitText.text = currentLimit.ToString();
            
            craftingStation.UpdateCraftLimit(currentLimit);
        }
        

        private void OnDropDownChanged(int index)
        {
            SetRecipe(craftingStation.recipeList[index]);
        }

        public void SetupUI(CraftingStation station)
        {
            if(craftingStation == station)
                return;
            
            craftingStation = station;
            dropdown.options.Clear();
            
            foreach (var recipe in craftingStation.recipeList)
            {
                TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
                optionData.text = recipe.craftingItem.itemSO.itemName;
                optionData.image = recipe.craftingItem.itemSO.sprite;
                dropdown.options.Add(optionData);
            }

            SetRecipe(craftingStation.selectedRecipe);
        }
        
        private void SetRecipe(Recipe recipe)
        {
            craftingStation.UpdateSelectedRecipe(recipe);
            
            for (int i = 0; i < reagentImages.Length; i++)
            {
                bool enableImage = recipe != null && recipe.reagents.Length > i;
                
                reagentImages[i].gameObject.SetActive(enableImage);
                if (enableImage)
                    reagentImages[i].sprite = recipe.reagents[i].itemSO.sprite;
            }
            
            craftedItemImage.gameObject.SetActive(recipe != null);
            if (recipe != null)
            {
                craftedItemImage.sprite = recipe.craftingItem.itemSO.sprite;
            }
        }
        
    }
}
