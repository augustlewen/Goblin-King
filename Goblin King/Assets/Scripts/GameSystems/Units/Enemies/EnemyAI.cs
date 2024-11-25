using System.Collections.Generic;
using GameSystems.Units.AI;
using GameSystems.Units.Goblins;
using UnityEngine;

namespace GameSystems.Units.Enemies
{
    public class EnemyAI : AINavMovement
    {
        public float sightRadius;
        public SphereCollider sightCollider;

        private float attackRadius;
        private float loseSightDistance;
        private readonly HashSet<GoblinStats> goblinsInSight = new();
        
        private void OnValidate()
        {
            if (sightCollider == null)
            {
                sightCollider = GetComponent<SphereCollider>();
            }

            if (sightCollider != null)
            {
                sightCollider.radius = sightRadius;
            }

            loseSightDistance = sightRadius * 1.25f;
        }

        private void Update()
        {
            foreach (var goblinStats in goblinsInSight)
            {
                var distance = Vector2.Distance(transform.position, goblinStats.transform.position);
                if (distance > loseSightDistance)
                {
                    goblinsInSight.Remove(goblinStats);
                }
            }
            
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var goblinStats = other.gameObject.GetComponent<GoblinStats>();
            if (goblinStats != null)
                goblinsInSight.Add(goblinStats);
        }
    }
}