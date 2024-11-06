using System.Collections;
using GameSystems.InteractableObjects;
using GameSystems.Items;
using GameSystems.Units.AI;
using UnityEngine;

namespace GameSystems.Units.Goblins
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
                // Move(new Vector2Int((int)mouseWorldPosition.x, (int)mouseWorldPosition.y));
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
                case Task.TaskType.BreakObject: StartCoroutine(BreakObject());
                    break;
            }
        }
      

        IEnumerator BreakObject()
        {
            Debug.Log("Begin Breaking Object");

            Task breakTask = currentTask;
            var breakableObj = currentTask.taskObject.GetComponent<BreakableObject>();
            ItemSO_Tool tool = stats.GetTool(breakableObj.toolRequired);
                
            while (true)
            {
                yield return new WaitForSeconds(tool.haste);

                if (breakableObj == null || currentTask != breakTask)
                    break;
                
                breakableObj.TakeDamage(tool.power);

                if (breakableObj == null)
                {
                    OnTaskComplete();
                    break;
                }
            }
        }

        private void OnTaskComplete()
        {
            state = State.FollowKing;
            currentTask = null;
        }
        public bool IsIdle()
        {
            return state is State.FollowKing or State.Idle;
        }
    }
}