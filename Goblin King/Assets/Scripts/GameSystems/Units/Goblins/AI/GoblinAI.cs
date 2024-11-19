using System.Collections;
using GameSystems.Items;
using GameSystems.Units.AI;
using UnityEngine;

namespace GameSystems.Units.Goblins.AI
{
    public class GoblinAI : AINavMovement
    {
        private Task currentTask;
        private State state;
        [HideInInspector] public GoblinStats stats; 
        
        private enum State
        {
            FollowKing,
            MoveToLocation,
            BreakingObject,
            Looting,
            Attacking,
            AssignedToStation,
            Idle
        }

        private void Awake()
        {
            stats = GetComponent<GoblinStats>();
        }

        public override void Update()
        {
            base.Update();
            
            if (IsIdle() && Input.GetMouseButtonDown(1))
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                SetDestination(mouseWorldPosition);
            }
        }

        public void AssignTask(Task newTask)
        {
            currentTask = newTask;
            
            Vector2 targetPosition = currentTask.taskObject.transform.position;
            Vector2 direction = ((Vector2)transform.position - targetPosition).normalized;
            float offsetDistance = 1.0f;
            targetPosition += direction * offsetDistance;
            SetDestination(targetPosition);
            
            state = State.MoveToLocation;
        }

        public override void OnReachDestination()
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

        // IEnumerator BreakObject()
        // {
        //     Debug.Log("Begin Breaking Object");
        //
        //     Task breakTask = currentTask;
        //     var breakableObj = currentTask.taskObject.GetComponent<BreakableObject>();
        //     ItemSO_Tool tool = stats.GetTool(breakableObj.toolRequired);
        //         
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(tool.haste);
        //
        //         if (breakableObj == null || currentTask != breakTask)
        //         {
        //             OnTaskComplete();
        //             break;
        //         }
        //         
        //         breakableObj.TakeDamage(tool.power);
        //
        //         if (breakableObj == null)
        //         {
        //             OnTaskComplete();
        //             break;
        //         }
        //     }
        // }

        public void OnTaskComplete()
        {
            state = State.FollowKing;
            currentTask = null;

            var newTask = GoblinManager.i.GetNewTask(this);
            if(newTask != null)
                AssignTask(newTask);
        }
        public bool IsIdle()
        {
            return state is State.FollowKing or State.Idle;
        }
    }
}