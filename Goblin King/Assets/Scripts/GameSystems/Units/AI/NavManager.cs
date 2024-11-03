using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace GameSystems.Units.AI
{
    public class NavManager : MonoBehaviour
    {
        private static NavManager i;
        private NavMeshSurface navMeshSurface;
        
        private void Awake()
        {
            i = this;
            navMeshSurface = GetComponent<NavMeshSurface>();
        }

        public static void Build()
        {
            i.navMeshSurface.BuildNavMesh();
            Debug.Log("UPDATE NAV MESH");
        }
    }
}