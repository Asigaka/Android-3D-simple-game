using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    private NavMeshAgent agent;
    [SerializeField] private Transform moveTarget;

    public Transform MoveTarget { get => moveTarget;
        set
        {
            moveTarget = value;
        }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void Update()
    {
        if (MoveTarget)
        {
            agent.SetDestination(MoveTarget.position);
        }
    }
}
