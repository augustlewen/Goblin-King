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

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
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
            SetDestination(currentTask.taskObject.transform.position);
            state = State.MoveToLocation;
        }

        private void OnReachDestination()
        {
            switch (currentTask.taskType)
            {
                case Task.TaskType.BreakObject:
                    state = State.BreakingObject;
                    StartCoroutine(BreakObject());
                    break;
            }
        }

        IEnumerator BreakObject()
        {
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
                    state = State.FollowKing;
                    break;
                }
            }
        }

        public bool IsIdle()
        {
            return state is State.FollowKing or State.Idle;
        }
    }
}