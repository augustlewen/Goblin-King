using System.Collections;
using GameSystems.GridObjects;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Items.SO;
using UnityEngine;

namespace GameSystems.Units.Goblins.AI
{
    public class Task
    {
        public readonly TaskObject taskObject;
        public readonly TaskType taskType;

        public enum TaskType
        {
            BreakObject,
            Loot,
            Assign,
            Attack
        }


        public Task(TaskType type, TaskObject taskObj)
        {
            taskType = type;
            taskObject = taskObj;
        }
        
        public IEnumerator BreakObject(GoblinAI ai)
        {
            Debug.Log("Begin Breaking Object");

            var breakableObj = taskObject.GetComponent<BreakableObject>();
            ItemToolData tool = ai.stats.GetTool(breakableObj.breakTool);
                
            while (true)
            {
                yield return new WaitForSeconds(tool.toolSO.haste);

                if(breakableObj != null)
                    breakableObj.TakeDamage(tool.toolSO.power);

                if (breakableObj == null)
                {
                    ai.OnTaskComplete();
                    break;
                }
            }
        }
        
    }
}