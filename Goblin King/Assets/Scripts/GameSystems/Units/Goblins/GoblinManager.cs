using System.Collections.Generic;
using GameSystems.GridObjects;
using GameSystems.Units.Goblins.AI;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class GoblinManager : MonoBehaviour
    {
        public static GoblinManager i;
        
        public GameObject goblinPrefab;
        public List<GoblinAI> goblins;
        public List<GoblinAI> goblinParty;

        private HashSet<Task> queuedTasks = new();
        
        private void Awake()
        {
            i = this;
        }

        private void Start()
        {
            var g1 = Instantiate(goblinPrefab, new Vector3(1, 1, 0), quaternion.identity, transform).GetComponent<GoblinAI>();
            var g2 = Instantiate(goblinPrefab, new Vector3(-1, -1, 0), quaternion.identity, transform).GetComponent<GoblinAI>();
            var g3 = Instantiate(goblinPrefab, new Vector3(-2, 1, 0), quaternion.identity, transform).GetComponent<GoblinAI>();

            
            goblins.Add(g1);
            goblins.Add(g2);
            goblins.Add(g3);

            goblinParty.Add(g1);
            goblinParty.Add(g2);
            goblinParty.Add(g3);
        }


        public void AddTask(Task task)
        {
            GoblinAI goblinAI = GetAppropriateGoblin(task);
            
            if(goblinAI != null)
                goblinAI.AssignTask(task);
            else
                queuedTasks.Add(task);
            
            
            // switch (task.taskType)
            // {
            //     case Task.TaskType.BreakObject: GetAppropriateGoblin(task.taskType, task.taskObject)?.AssignTask(task);
            //         break;
            // }
        }

        private GoblinAI GetAppropriateGoblin(Task task)
        {
            List<GoblinAI> viableGoblins = new();
            GoblinAI appropriateGoblin;
            
            // if (taskType == Task.TaskType.BreakObject)
            // {
            //     BreakableObject breakable = taskObject.GetComponent<BreakableObject>();
            //     
            //     foreach (var goblinAI in goblinParty)
            //         if(goblinAI.IsIdle() && goblinAI.stats.HasToolType(breakable.toolRequired))
            //             viableGoblins.Add(goblinAI);
            // }
            foreach (var goblinAI in goblinParty)
                if(goblinAI.IsIdle() && IsGoblinViableForTask(goblinAI, task))
                    viableGoblins.Add(goblinAI);
            
            if (viableGoblins.Count == 0)
            {
                Debug.LogWarning("No Viable Goblins To Be Assigned");
                return null;
            }

            appropriateGoblin = viableGoblins[0];
            float shortestDistance =
                Vector2.Distance(viableGoblins[0].transform.position, task.taskObject.transform.position);
                
            foreach (var goblin in viableGoblins)
            {
                // float newDistance =
                //     Vector2.Distance(goblin.transform.position, task.taskObject.transform.position);
                //
                // if (newDistance < shortestDistance)
                // {
                //     shortestDistance = newDistance;
                //     appropriateGoblin = goblin;
                // }
                if (!isShorterDistance(shortestDistance, goblin.transform, task.taskObject.transform,
                        out float newDistance)) continue;
                shortestDistance = newDistance;
                appropriateGoblin = goblin;

            }

            return appropriateGoblin;
        }

        private bool isShorterDistance(float distance, Transform trans1, Transform trans2, out float newDistance)
        {
            newDistance = Vector2.Distance(trans1.position, trans2.position);
            return newDistance < distance;
        }
        
        public Task GetNewTask(GoblinAI goblinAI)
        {
            float taskDistance = float.MaxValue;
            Task appropriateTask = null;

            foreach (var task in queuedTasks)
            {
                if (IsGoblinViableForTask(goblinAI, task))
                {
                    // float newTaskDistance = Vector2.Distance(goblinAI.transform.position, task.taskObject.transform.position);
                    // if (newTaskDistance < taskDistance)
                    //     appropriateTask = task;
                    if (!isShorterDistance(taskDistance, goblinAI.transform, task.taskObject.transform,
                            out float newDistance)) continue;
                    taskDistance = newDistance;
                    appropriateTask = task;
                }
            }

            if (appropriateTask != null)
                queuedTasks.Remove(appropriateTask);
            
            return appropriateTask;
        }
        
        private bool IsGoblinViableForTask(GoblinAI goblinAI, Task task)
        {
            if (task.taskType == Task.TaskType.BreakObject)
            {
                BreakableObject breakable = task.taskObject.GetComponent<BreakableObject>();
                if (goblinAI.stats.HasToolType(breakable.breakTool))
                    return true;
            }
            
            return false;
        }
        
        
        
    }
}