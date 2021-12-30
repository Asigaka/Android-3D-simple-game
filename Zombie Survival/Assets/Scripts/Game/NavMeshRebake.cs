using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebake : MonoBehaviour
{
    private NavMeshSurface nav;

    private void Start()
    {
        nav = GetComponent<NavMeshSurface>();
        nav.BuildNavMesh();
    }
}
