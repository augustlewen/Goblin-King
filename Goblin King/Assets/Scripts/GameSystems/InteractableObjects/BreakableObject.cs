using GameSystems.Items;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.InteractableObjects
{
    public class BreakableObject : MonoBehaviour
    {
        public ItemSO_Tool.ToolType itemInteraction;
        public ItemSO dropItem;
        public int hp;

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