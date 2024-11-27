using System;
using System.Collections.Generic;
using GameSystems.Units.AI;
using GameSystems.Units.Goblins;
using GameSystems.Units.King;
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
                sightCollider = GetComponent<SphereCollider>();
            else
                sightCollider.radius = sightRadius;

            loseSightDistance = sightRadius * 1.25f;
        }

        public override void Update()
        {
            float distanceFromKing = Vector2.Distance(transform.position, KingMovement.i.transform.position);
            if (distanceFromKing > 20)
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
            
            base.Update();
            
            foreach (var goblinStats in goblinsInSight)
            {
                var distance = Vector2.Distance(transform.position, goblinStats.transform.position);
                if (distance > loseSightDistance)
                {
                    goblinsInSight.Remove(goblinStats);
                }
            }

            if (goblinsInSight.Count > 0)
                combatAI.SetTarget(GetBestTarget());
            else
                combatAI.SetTarget(null);
        }

        GoblinStats GetBestTarget()
        {
            GoblinStats closestGoblin = null;
            float closestDistance = Mathf.Infinity;

            foreach (var goblin in goblinsInSight)
            {
                float distance = Vector3.Distance(transform.position, goblin.transform.position);
            
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGoblin = goblin;
                }
            }

            return closestGoblin;
        }

        private void OnTriggerEnter(Collider other)
        {
            var goblinStats = other.gameObject.GetComponent<GoblinStats>();
            if (goblinStats != null)
                goblinsInSight.Add(goblinStats);
        }
        
    }
}