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
            Attack,
            Build
        }


        public Task(TaskType type, TaskObject taskObj)
        {
            taskType = type;
            taskObject = taskObj;
        }
        
        public IEnumerator BreakObject(GoblinAI ai)
        {
            var breakableObj = taskObject.GetComponent<BreakableObject>();
            ItemToolData tool = ai.stats.GetTool(breakableObj.breakTool);
                
            while (true)
            {
                yield return new WaitForSeconds(tool.toolSO.haste);
                
                ai.stats.PlayItemAnimation();
                
                if(breakableObj != null)
                    breakableObj.TakeDamage(tool.toolSO.power);

                if (breakableObj.hp <= 0)
                {
                    ai.OnTaskComplete();
                    break;
                }
            }
        }

        public IEnumerator Build(GoblinAI ai)
        {
            var buildProject = taskObject.GetComponent<BuildProject>();
                
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                
                ai.stats.PlayItemAnimation();
                
                if(buildProject != null)
                    buildProject.buildDuration -= 0.5f;

                if (buildProject.buildDuration <= 0)
                {
                    buildProject.CompleteBuild();
                    ai.OnTaskComplete();
                    break;
                }
            }
        }
        
    }
}