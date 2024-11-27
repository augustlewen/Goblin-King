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
        private Transform enemyParent;
        private readonly HashSet<GameObject> enemies = new();
        [FormerlySerializedAs("maxEnemies")] public int maxActiveEnemies;

        public Vector2 minSpawnOffset;
        
        private void Awake()
        {
            foreach (Transform child in enemyParent)
                enemies.Add(child.gameObject);
            
            StartCoroutine(Spawner());

        }

        IEnumerator Spawner()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);

                if (enemies.Count(enemy => enemy.activeSelf) >= maxActiveEnemies) 
                    continue;
                
                Vector2 kingPos = KingMovement.i.transform.position;
                var area = KingMovement.i.navArea.size;

                float xOffset = Random.Range(minSpawnOffset.x, area.x);
                float yOffset = Random.Range(minSpawnOffset.y, area.z);
                xOffset *= Random.value > 0.5f ? 1 : -1;
                yOffset *= Random.value > 0.5f ? 1 : -1;

                ObjectPooling.ActivateObject(enemies, kingPos + new Vector2(xOffset, yOffset));
            }
        }
    }
}