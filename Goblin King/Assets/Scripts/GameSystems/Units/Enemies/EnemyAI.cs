using System;
using System.Collections.Generic;
using System.Linq;
using GameSystems.Units.AI;
using GameSystems.Units.Goblins;
using GameSystems.Units.King;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameSystems.Units.Enemies
{
    public class EnemyAI : AINavMovement
    {
        public float sightRadius;
        // public SphereCollider sightCollider;

        private float attackRadius;
        private float loseSightDistance;
        List<GoblinStats> goblinsInSight = new();
        
        private void OnValidate()
        {
            // if (sightCollider == null)
            //     sightCollider = GetComponent<SphereCollider>();
            // else
            //     sightCollider.radius = sightRadius;

            // loseSightDistance = sightRadius * 1.25f;
        }

        protected override void Awake()
        {
            base.Awake();
            loseSightDistance = sightRadius * 1.25f;

        }

        public override void Update()
        {
            float distanceFromKing = Vector2.Distance(transform.position, KingMovement.i.transform.position);
            if (distanceFromKing > 20)
            {
                gameObject.SetActive(false);
                return;
            }
            
            base.Update();

            var colliders = Physics.OverlapSphere(transform.position, sightRadius, LayerMask.GetMask("Unit"));
            goblinsInSight.Clear();

            foreach (var overlapCollider in colliders)
            {
                GoblinStats goblinStats = overlapCollider.gameObject.GetComponent<GoblinStats>();
                if (goblinStats == null)
                    continue;

                goblinsInSight.Add(goblinStats);
            }
            
            if(goblinsInSight.Count == 0)
                combatAI.SetTarget(null);
            else
                combatAI.SetTarget(GetBestTarget());
        }

        GoblinStats GetBestTarget()
        {
            GoblinStats closestGoblin = null;
            float closestDistance = Mathf.Infinity;
            
            foreach (var goblin in goblinsInSight)
            {
                float distance = Vector3.Distance(transform.position, goblin.transform.position);

                if (!(distance < closestDistance)) 
                    continue;
                
                closestDistance = distance;
                closestGoblin = goblin;
            }

            return closestGoblin;
        }

        // private void OnTriggerEnter(Collider other)
        // {
        //     var goblinStats = other.gameObject.GetComponent<GoblinStats>();
        //     if (goblinStats != null)
        //     {
        //         goblinsInSight.Add(goblinStats);
        //     }
        // }
    }
}