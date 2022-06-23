using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private EnemyInfo info;
    private NavMeshAgent agent;

    public EnemyInfo Info { get => info;
        set
        {
            info = value;
            agent.stoppingDistance = value.AttackDistance;
        }
    }

    public NavMeshAgent Agent { get => agent; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget(Transform target)
    {
        if (agent.enabled)
            agent.SetDestination(target.position);
    }
}
