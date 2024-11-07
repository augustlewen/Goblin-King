using System;
using GameSystems.Items;
using GameSystems.Units.Goblins;
using GameSystems.Units.Goblins.AI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.InteractableObjects
{
    public class BreakableObject : MonoBehaviour, ISelect
    {
        public ItemSO_Tool.ToolType toolRequired;
        public ItemSO dropItem;
        public int hp;

        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnSelect()
        {
            GoblinManager.i.AssignTask(new Task(Task.TaskType.BreakObject, gameObject));
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
            var item = Instantiate(new GameObject(), transform.position, quaternion.identity);
            item.AddComponent<DroppedItem>().itemSO = dropItem;
            
            Destroy(gameObject);
        }

    }
}