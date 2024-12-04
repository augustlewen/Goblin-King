using System;
using GameSystems.Items.SO;

namespace GameSystems.Items
{
    [Serializable]
    public class Recipe
    {
        public ItemObject[] items;
    }

    [Serializable]
    public class ItemObject
    {
        public ItemSO itemSO;
        public int count;
    }
}