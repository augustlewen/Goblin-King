using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using UnityEngine;

namespace GameSystems.Interactions
{
    public class TaskObject : GridObject, ISelect
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