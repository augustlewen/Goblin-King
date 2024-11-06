using System.Collections;
using GameSystems.InteractableObjects;
using GameSystems.Items;
using UnityEngine;

namespace GameSystems.Units.Goblins.AI
{
    public class Task
    {
        public readonly GameObject taskObject;
        public readonly TaskType taskType;

        public enum TaskType
        {
            BreakObject,
            Loot,
            Assign,
            Attack
        }


        public Task(TaskType type, GameObject taskObj)
        {
            taskType = type;
            taskObject = taskObj;
        }
        
        public IEnumerator BreakObject(GoblinAI ai)
        {
            Debug.Log("Begin Breaking Object");

            var breakableObj = taskObject.GetComponent<BreakableObject>();
            ItemSO_Tool tool = ai.stats.GetTool(breakableObj.toolRequired);
                
            while (true)
            {
                yield return new WaitForSeconds(tool.haste);

                if(breakableObj != null)
                    breakableObj.TakeDamage(tool.power);

                if (breakableObj == null)
                {
                    ai.OnTaskComplete();
                    break;
                }
            }
        }
        
    }
}