using System;
using Unity.Mathematics;
using UnityEngine;

namespace GameSystems.Units.Goblins
{
    public class GoblinManager : MonoBehaviour
    {
        public GameObject goblinPrefab;

        private void Start()
        {
            Instantiate(goblinPrefab, new Vector3(1, 1, 0), quaternion.identity, transform);
            Instantiate(goblinPrefab, new Vector3(-1, -1, 0), quaternion.identity, transform);
        }
    }
}