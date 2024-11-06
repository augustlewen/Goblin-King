using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class Task
    {
        public GameObject taskObject;
        public TaskType taskType;

        public enum TaskType
        {
            BreakObject,
            Loot,
            Assign,
            Attack
        }


        public Task(TaskType type, GameObject taskObj)
        {
            taskType = type;
            taskObject = taskObj;
        }
    }
}