using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using UnityEngine;

namespace GameSystems.InteractableObjects
{
    public class TaskObject : MonoBehaviour, ISelect
    {
        [HideInInspector] public Task.TaskType taskType;
        private bool isTask;
        
        public virtual void OnSelect()
        {
            if (!isTask)
            {
                GoblinManager.i.AddTask(new Task(taskType, this));
                isTask = true;
            }
        }
        
        
    }
}