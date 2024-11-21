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
        // private State state;
        [HideInInspector] public GoblinStats stats;
        
        public float kingOffsetDistance;
        private bool isIdle;
        
        // private enum State
        // {
        //     FollowKing,
        //     MoveToLocation,
        //     BreakingObject,
        //     Looting,
        //     Attacking,
        //     AssignedToStation,
        //     Idle
        // }

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
            
            Vector2 kingPosition = KingMovement.i.transform.position;
            Vector2 directionToKing = (kingPosition - (Vector2)transform.position).normalized;

            Vector2 offset = directionToKing * kingOffsetDistance;
            Vector2 destination = kingPosition - offset;

            SetDestination(destination);
        }

        public override void Update()
        {
            base.Update();
            
            
            // if (IsIdle() && Input.GetMouseButtonDown(1))
            // {
            //     Vector3 mouseScreenPosition = Input.mousePosition;
            //     Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            //     SetDestination(mouseWorldPosition);
            // }
        }

        public void AssignTask(Task newTask)
        {
            currentTask = newTask;
            
            Vector2 targetPosition = currentTask.taskObject.transform.position;
            Vector2 direction = ((Vector2)transform.position - targetPosition).normalized;
            float offsetDistance = 1.0f;
            targetPosition += direction * offsetDistance;
            SetDestination(targetPosition);

            isIdle = false;
            // state = State.MoveToLocation;
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