using System.Collections.Generic;
using GameSystems.GridObjects;
using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Items.SO;
using GameSystems.Units.AI;
using GameSystems.Units.Goblins.AI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Units.Goblins
{
    public class GoblinTaskManager : MonoBehaviour
    {
        public static GoblinTaskManager i;
        [HideInInspector]public UnityEvent OnSpawnedGoblins = new();
        
        public GameObject goblinPrefab;
        public List<GoblinAI> goblins;
        public List<GoblinAI> goblinParty;

        private HashSet<Task> queuedTasks = new();

        [SerializeField] private List<ItemSO> startItems = new();
        
        
        private void Awake()
        {
            i = this;
            FindAnyObjectByType<ChunkNavBaker>().OnNavMeshBuilt.AddListener(SpawnGoblins);
        }

        private void SpawnGoblins()
        {
            for (int i = 0; i < startItems.Count; i++)
            {
                var goblin = Instantiate(goblinPrefab, new Vector3(i, i, 0), quaternion.identity, transform).GetComponentInChildren<GoblinAI>();
                goblins.Add(goblin);
                goblinParty.Add(goblin);

                goblin.stats.EquipItem(startItems[i].CreateItemData());
            }
            
            FindAnyObjectByType<ChunkNavBaker>().OnNavMeshBuilt.RemoveListener(SpawnGoblins);
            OnSpawnedGoblins.Invoke();
        }


        public void AddTask(Task task)
        {
            if (task.taskType == Task.TaskType.Attack)
            {
                var viableGoblins = GetAllViableGoblins(task);
                foreach (var goblin in viableGoblins)
                    goblin.AssignTask(task);
                return;
            }
                
            GoblinAI goblinAI = GetAppropriateGoblin(task);

            if (goblinAI != null)
            {
                goblinAI.AssignTask(task);
            }
            else
                queuedTasks.Add(task);
        }
        
        private List<GoblinAI> GetAllViableGoblins(Task task)
        {
            List<GoblinAI> viableGoblins = new();
            
            foreach (var goblinAI in goblinParty)
                if(goblinAI.IsIdle() && IsGoblinViableForTask(goblinAI, task))
                    viableGoblins.Add(goblinAI);

            return viableGoblins;
        }

        private GoblinAI GetAppropriateGoblin(Task task)
        {
            List<GoblinAI> viableGoblins = GetAllViableGoblins(task);
            
            if (viableGoblins.Count == 0)
            {
                Debug.LogWarning("No Viable Goblins To Be Assigned to : " + task.taskType);
                return null;
            }

            GoblinAI appropriateGoblin = viableGoblins[0];
            float shortestDistance =
                Vector2.Distance(appropriateGoblin.transform.position, task.taskObject.transform.position);
                
            foreach (var goblin in viableGoblins)
            {
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
        
        // ReSharper disable Unity.PerformanceAnalysis
        private bool IsGoblinViableForTask(GoblinAI goblinAI, Task task)
        {
            if (!goblinAI.CanMoveTo(task.taskObject.transform.position))
                return false;
            
            switch (task.taskType)
            {
                case Task.TaskType.BreakObject : BreakableObject breakable = task.taskObject.GetComponent<BreakableObject>();
                        return goblinAI.stats.HasTool(breakable.breakTool);
                case Task.TaskType.Loot : return goblinAI.stats.bag != null;
                case Task.TaskType.Attack : return goblinAI.stats.HasWeapon();
                case Task.TaskType.Build : return goblinAI.stats.HasTool(ToolType.Hammer);
            }
            return false;
        }

        
        
        
        
    }
}