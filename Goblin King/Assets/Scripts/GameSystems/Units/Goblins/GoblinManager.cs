using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.InteractableObjects;
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


        public void AssignTask(Task task)
        {
            switch (task.taskType)
            {
                case Task.TaskType.BreakObject: GetAppropriateGoblin(task.taskType, task.taskObject)?.AssignTask(task);
                    break;
            }
        }

        private GoblinAI GetAppropriateGoblin(Task.TaskType taskType, GameObject taskObject)
        {
            List<GoblinAI> viableGoblins = new();
            GoblinAI appropriateGoblin;
            
            if (taskType == Task.TaskType.BreakObject)
            {
                BreakableObject breakable = taskObject.GetComponent<BreakableObject>();
                
                foreach (var goblinAI in goblinParty)
                    if(goblinAI.IsIdle() && goblinAI.stats.HasToolType(breakable.toolRequired))
                        viableGoblins.Add(goblinAI);
            }
            
            
            if (viableGoblins.Count == 0)
            {
                Debug.LogWarning("No Viable Goblins To Be Assigned");
                return null;
            }

            appropriateGoblin = viableGoblins[0];
            float shortestDistance =
                Vector2.Distance(viableGoblins[0].transform.position, taskObject.transform.position);
                
            foreach (var goblin in viableGoblins)
            {
                float newDistance =
                    Vector2.Distance(goblin.transform.position, taskObject.transform.position);

                if (newDistance < shortestDistance)
                {
                    shortestDistance = newDistance;
                    appropriateGoblin = goblin;
                }
            }

            return appropriateGoblin;
        }
        
        
        
    }
}