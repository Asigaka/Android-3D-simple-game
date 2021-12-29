using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class ZombieMovementController : MonoBehaviour, ICreature
{
    [SerializeField] private NavMeshAgent agent;

    private Vector3 targetPosition;
    public Vector3 lastTargetPos;

    private FieldOfView fov;

    private enum ZombieState { Idle, WalkToTarget, PursueTarget}
    [SerializeField]
    private ZombieState currentState = ZombieState.Idle;
    private ZombieState previousState;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        ZombieStateMachine();
    }

    private void ZombieStateMachine()
    {
        fov.CheckHumanAround();
        
        switch (currentState)
        {
            case ZombieState.Idle:
                if (fov.IsSeeHuman)
                {
                    SetState(ZombieState.PursueTarget);
                }
                else if (!fov.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    SetState(ZombieState.WalkToTarget);
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
            case ZombieState.WalkToTarget:
                WalkToLastTarget();
                if (fov.IsSeeHuman)
                {
                    SetState(ZombieState.PursueTarget);
                }
                else if (!fov.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    return;
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
            case ZombieState.PursueTarget:
                if (fov.IsSeeHuman)
                {
                    OnSeePlayer();
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
        }
    }

    private void SetState(ZombieState to)
    {
        if (previousState != currentState)
            previousState = currentState;
        
        currentState = to;
    }

    public void OnHeardNoise(Vector3 noisePos)
    {
        if (!fov.IsSeeHuman &&
            noisePos.x != 0 && noisePos.y != 0 && noisePos.z != 0)
        {
            lastTargetPos = noisePos;
            SetState(ZombieState.WalkToTarget);
        }
    }

    private void WalkToLastTarget()
    {
        if (lastTargetPos.x != 0 && lastTargetPos.y != 0 && lastTargetPos.z != 0)
        {
            agent.SetDestination(lastTargetPos);
        }
    }

    public void OnMetObstacle()
    {
        throw new System.NotImplementedException();
    }

    public void OnSeePlayer()
    {
        targetPosition = fov.HumanPos;
        agent.SetDestination(targetPosition);
        if (lastTargetPos != targetPosition)
            lastTargetPos = targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(lastTargetPos, 0.5f);
    }
}
