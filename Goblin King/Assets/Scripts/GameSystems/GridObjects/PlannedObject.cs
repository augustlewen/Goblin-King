using System.Collections.Generic;
using System.Linq;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class PlannedObject : TaskObject
    {
        private GridObjectSO gridObjectSO;
        private List<ItemObject> itemObjects;

        public void Setup(GridObjectSO goso)
        {
            var task = new Task(Task.TaskType.Build, this);
            gridObjectSO = goso;
            GoblinManager.i.AddTask(task);
        }

        public void AddItem(ItemObject itemObject)
        {
            var existingItemObj = itemObjects.FirstOrDefault(item => item.itemSO == itemObject.itemSO);
            if (existingItemObj != null)
                existingItemObj.count += itemObject.count;
            else
            {
                itemObjects.Add(itemObject);
            }
        }
        
        
        
        public Recipe GetRecipe()
        {
            return gridObjectSO.recipe;
        }
        
    }
}