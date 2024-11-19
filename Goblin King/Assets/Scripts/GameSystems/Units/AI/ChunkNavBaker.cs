using System;
using System.Collections;
using System.Collections.Generic;
using GameSystems.Units.King;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

namespace GameSystems.Units.AI
{
    public class ChunkNavBaker : MonoBehaviour
    {
        public UnityEvent OnNavMeshBuilt = new();
        
        public NavMeshSurface surface;
        public KingMovement king;
        public float updateRate = 0.1f;
        public float movementThreshold = 3;
        public Vector3 navMeshSize = new Vector3(25, 1, 25);

        private Vector3 worldAnchor;
        private NavMeshData navMeshData;
        private List<NavMeshBuildSource> sources = new();

        private void Awake()
        {
            navMeshData = new NavMeshData();
            NavMesh.AddNavMeshData(navMeshData);
            surface.navMeshData = navMeshData; // Assign the navMeshData to the surface
            StartCoroutine(CheckPlayerMovement());
        }
        

        private void Start()
        {
            BuildNavMesh(false);
        }

        private IEnumerator CheckPlayerMovement()
        {
            WaitForSeconds wait = new WaitForSeconds(updateRate);

            while (true)
            {
                if (Vector3.Distance(worldAnchor, king.transform.position) > movementThreshold)
                {
                    BuildNavMesh(true);
                    worldAnchor = king.transform.position;
                }

                yield return wait;
            }
        }

        private void BuildNavMesh(bool async)
        {
            Bounds navMeshBounds = new Bounds(king.transform.position, navMeshSize);
            List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();
            
            List<NavMeshModifier> modifiers = surface.collectObjects == CollectObjects.Children ? 
                new List<NavMeshModifier>(surface.GetComponentsInChildren<NavMeshModifier>()) : NavMeshModifier.activeModifiers;

            foreach (var t in modifiers)
            {
                if((surface.layerMask & (1 << t.gameObject.layer)) == 1 
                   && t.AffectsAgentType(surface.agentTypeID))
                {
                    markups.Add(new NavMeshBuildMarkup()
                    {
                        root = t.transform,
                        overrideArea = t.overrideArea,
                        area = t.area,
                        ignoreFromBuild = t.ignoreFromBuild
                    });
                }
            }

            if (surface.collectObjects == CollectObjects.Children)
                NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, sources);
            else
                NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, sources);
                
            // sources.RemoveAll(source =>
            //     source.component != null && source.component.gameObject.GetComponents<NavMeshAgent>() != null);
            
            
            if (async)
            {
                NavMeshBuilder.UpdateNavMeshDataAsync(navMeshData, surface.GetBuildSettings(), sources,
                    new Bounds(king.transform.position, navMeshSize));
            }
            else
            {
                NavMeshBuilder.UpdateNavMeshData(navMeshData, surface.GetBuildSettings(), sources,
                    new Bounds(king.transform.position, navMeshSize));
            }
            
            surface.BuildNavMesh();
            OnNavMeshBuilt.Invoke();

        }
    }
    
}