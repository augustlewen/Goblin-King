using System.Collections;
using GameSystems.Items;
using GameSystems.Units.AI;
using GameSystems.Units.King;
using Unity.VisualScripting;
using UnityEngine;

namespace GameSystems.Units.Goblins.AI
{
    public class GoblinAI : AINavMovement
    {
        private Task currentTask;
        public GoblinStats stats;

        public float interactOffsetDistance;
        public float kingOffsetDistance;
        private bool isIdle;

        protected override void Awake()
        {
            base.Awake();
            isIdle = true;
        }

        protected void Start()
        {
            KingMovement.i.OnMoveUpdate.AddListener(OnKingMoveUpdate);
        }
        
        

        private void OnKingMoveUpdate()
        {
            if (!isIdle) 
                return;

            Vector3 randomOffset = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
            SetDestination( KingMovement.i.transform.position + randomOffset, kingOffsetDistance);
        }
        

        public void AssignTask(Task newTask)
        {
            currentTask = newTask;
            
            Vector2 targetPosition = currentTask.taskObject.transform.position;
            SetDestination(targetPosition, interactOffsetDistance);
            isIdle = false;
        }

        protected override void ReachedDestination()
        {
            base.ReachedDestination();

            if(currentTask == null)
                return;
            
            switch (currentTask.taskType)
            {
                case Task.TaskType.BreakObject: StartCoroutine(currentTask.BreakObject(this));
                    break;
                case Task.TaskType.Loot: currentTask.taskObject.GetComponent<Loot>().LootItems(stats.bag);
                    OnTaskComplete();
                    break;
            }
        }

        public void OnTaskComplete()
        {
            currentTask = null;

            var newTask = GoblinManager.i.GetNewTask(this);
            if(newTask != null)
                AssignTask(newTask);
            else
                isIdle = true;
        }
        public bool IsIdle()
        {
            return isIdle;
        }
    }
}