using GameSystems.GridObjects;
using GameSystems.Items;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Crafting
{
    public class CraftingUI : MonoBehaviour
    {
        public Recipe selectedRecipe;
        public Image[] reagentImages;
        public Image craftedItemImage;

        public void SetupUI(CraftingStation craftingStation)
        {
            
        }
        
        public void SetRecipe(Recipe recipe)
        {
            selectedRecipe = recipe;

            for (int i = 0; i < reagentImages.Length; i++)
            {
                reagentImages[i].gameObject.SetActive(recipe.reagents.Length > i);

                if (recipe.reagents.Length > i)
                {
                    reagentImages[i].sprite = recipe.reagents[i].itemSO.sprite;
                }
            }
        }
        
    }
}
