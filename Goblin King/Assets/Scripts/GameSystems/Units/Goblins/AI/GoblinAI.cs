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
        [HideInInspector] public GoblinStats stats;

        public float interactOffsetDistance;
        public float kingOffsetDistance;
        private bool isIdle;

        private void Awake()
        {
            stats = GetComponent<GoblinStats>();
            isIdle = true;
        }

        protected override void Start()
        {
            base.Start();
            KingMovement.i.OnMoveUpdate.AddListener(OnKingMoveUpdate);
        }

        private void OnKingMoveUpdate()
        {
            if (!isIdle) 
                return;
            
            SetDestination( KingMovement.i.transform.position, kingOffsetDistance);
        }
        

        public void AssignTask(Task newTask)
        {
            currentTask = newTask;
            
            Vector2 targetPosition = currentTask.taskObject.transform.position;
            SetDestination(targetPosition, interactOffsetDistance);
            isIdle = false;
        }

        protected override void OnReachDestination()
        {
            base.OnReachDestination();

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