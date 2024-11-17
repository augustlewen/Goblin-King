using GameSystems.Interactions;
using GameSystems.Items;
using GameSystems.Items.SO;
using GameSystems.Units.Goblins.AI;
using GameSystems.World.Grid;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.GridObjects
{
    public class BreakableObject : TaskObject
    {
        public ToolType breakTool;
        public DropTable dropTable;
        public int hp;

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            taskType = Task.TaskType.BreakObject;
        }

        public override void Setup(GridObjectSO gridObjectSO)
        {
            base.Setup(gridObjectSO);
            
            if (gridObjectSO is GOSO_Breakable gosoBreakable)
            {
                hp = gosoBreakable.hp;
                breakTool = gosoBreakable.breakTool;
                dropTable = gosoBreakable.dropTable;
            }
        }

        public override void OnSelect()
        {
            base.OnSelect();
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
            // var item = Instantiate(new GameObject(), transform.position, quaternion.identity);
            // item.AddComponent<DroppedItem>().item = dropItem;
            dropTable.DropItems(transform.position);
            gameObject.SetActive(false);
        }

    }
}