using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Items.SO;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.GridObjects
{
    public class BreakableObject : MonoBehaviour
    {
        public ToolType breakTool;
        public LootTable lootTable;
        public int hp;

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var taskObj = gameObject.AddComponent<TaskObject>();
            taskObj.taskType = Task.TaskType.BreakObject;
            taskObj.OnSelectTask.AddListener(OnSelectTask);
        }

        public void Setup(GridObjectSO gridObjectSO)
        {
            if (gridObjectSO is GOSO_Breakable gosoBreakable)
            {
                hp = gosoBreakable.hp;
                breakTool = gosoBreakable.breakTool;
                lootTable = gosoBreakable.lootTable;
            }
        }
        
        
        private void OnSelectTask()
        {
            spriteRenderer.color = new Color(0.4f, 0.5f, 0.65f, 1);
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                Break();
            }
        }

        private void Break()
        {
            lootTable.DropItems(transform.position);
            gameObject.SetActive(false);
        }

    }
}