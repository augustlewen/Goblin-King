using System.Collections.Generic;
using System.Linq;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using Specific_Items;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.GridObjects
{
    public class PlannedObject : TaskObject
    {
        private GridObjectSO gridObjectSO;
        private List<ItemObject> itemObjects;

        public GridObject gridObjectPrefab;

        public void Setup(GridObjectSO goso)
        {
            var task = new Task(Task.TaskType.Build, this);
            gridObjectSO = goso;
            GoblinManager.i.AddTask(task);
            GetComponent<SpriteRenderer>().sprite = goso.sprite;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1, 0.8f);
        }

        public void AddItem(ItemObject itemObject)
        {
            var existingItemObj = itemObjects.FirstOrDefault(item => item.itemSO == itemObject.itemSO);
            if (existingItemObj != null)
                existingItemObj.count += itemObject.count;
            else
                itemObjects.Add(itemObject);

            if (gridObjectSO.recipe.IsRequirementsMet(itemObjects))
            {
                //Replace with GridObject!
                var gridObject = Instantiate(gridObjectPrefab, transform.position, quaternion.identity);
                gridObject.Setup(gridObjectSO);
                Destroy(gameObject);
            }
        }
        
        public Recipe GetRecipe()
        {
            return gridObjectSO.recipe;
        }
        
    }
}