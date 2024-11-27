using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Functions;
using GameSystems.Units.King;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GameSystems.Units.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public float spawnRate;
        public Transform enemyParent;
        private readonly HashSet<GameObject> enemies = new();
        public int maxActiveEnemies;

        public Vector2 minSpawnOffset;
        
        private void Start()
        {
            foreach (Transform child in enemyParent)
                enemies.Add(child.gameObject);
            
            StartCoroutine(Spawner());
        }

        IEnumerator Spawner()
        {
            while (true)
            {
                if (enemies.Count(enemy => enemy.activeSelf) >= maxActiveEnemies)
                    yield return new WaitForSeconds(spawnRate);
                
                Vector2 kingPos = KingMovement.i.transform.position;
                var area = KingMovement.i.navArea.size;

                float xOffset = Random.Range(minSpawnOffset.x, area.x / 2 - 1);
                float yOffset = Random.Range(minSpawnOffset.y, area.z / 2 - 1);
                xOffset *= Random.value > 0.5f ? 1 : -1;
                yOffset *= Random.value > 0.5f ? 1 : -1;

                var go = ObjectPooling.ActivateObject(enemies, kingPos + new Vector2(xOffset, yOffset));
                yield return new WaitForSeconds(spawnRate);
            }
        }
    }
}