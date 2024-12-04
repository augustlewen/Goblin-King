using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Items.SO;

namespace GameSystems.Items
{
    [Serializable]
    public class Recipe
    {
        public ItemObject[] items;

        public bool IsRequirementsMet(List<ItemObject> objects)
        {
            foreach (var recipeItem in items)
            {
                if (!objects.Any(item => item.itemSO == recipeItem.itemSO && item.count >= recipeItem.count))
                    return false;
            }

            return true;
        }
    }

    [Serializable]
    public class ItemObject
    {
        public ItemSO itemSO;
        public int count;
    }
}