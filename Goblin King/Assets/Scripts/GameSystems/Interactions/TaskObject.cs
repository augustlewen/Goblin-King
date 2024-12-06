using System;
using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystems.Interactions
{
    public class TaskObject : MonoBehaviour, ISelect
    {
        [HideInInspector] public readonly UnityEvent OnSelectTask = new();
        public Task.TaskType taskType;
        private bool isTask;

        private void Awake()
        {
            gameObject.AddComponent<MouseInteractable>();
        }

        public virtual void SelectObject()
        {
            if (!isTask)
            {
                GoblinManager.i.AddTask(new Task(taskType, this));
                isTask = true;
                OnSelectTask.Invoke();
            }
        }
        
        
    }
}