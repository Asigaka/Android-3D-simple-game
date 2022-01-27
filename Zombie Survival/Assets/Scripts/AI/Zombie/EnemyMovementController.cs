using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour, ICreature
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnimations anim;
    [SerializeField] private EnemyCombatController combatController;
    [SerializeField] private FieldOfView movementView;

    private Vector3 targetPosition;
    private Vector3 lastTargetPos;

    private enum EnemyState { Idle, WalkToTarget, PursueTarget, Attack, Die}

    [SerializeField] private EnemyState currentState = EnemyState.Idle;
    private EnemyState previousState;

    private void Update()
    {
        EnemyStateMachine();
    }

    private void EnemyStateMachine()
    {
        movementView.CheckHumanAround();
        
        switch (currentState)
        {
            case EnemyState.Idle:
                anim.SetWalk(false);
                if (movementView.IsSeeHuman)
                {
                    SetState(EnemyState.PursueTarget);
                }
                else if (!movementView.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    SetState(EnemyState.WalkToTarget);
                }
                else
                {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.WalkToTarget:
                anim.SetWalk(true);
                if (combatController.CombatView.IsSeeHuman)
                {
                    SetState(EnemyState.Attack);
                }

                WalkToLastTarget();
                if (movementView.IsSeeHuman)
                {
                    SetState(EnemyState.PursueTarget);
                }
                else if (!movementView.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    return;
                }
                else
                {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.PursueTarget:
                anim.SetWalk(true);
                if (agent.stoppingDistance >= agent.remainingDistance)
                {
                    SetState(EnemyState.Attack);
                }

                if (movementView.IsSeeHuman)
                {
                    OnSeePlayer();
                }
                else
                {
                    SetState(EnemyState.Idle);
                }
                break;
            case EnemyState.Attack:
                anim.SetWalk(false);
                combatController.Attack();
                break;
            case EnemyState.Die:
                anim.DieTrigger();
                Destroy(gameObject, 4f);
                break;
        }
    }

    private void SetState(EnemyState to)
    {
        if (previousState != currentState)
            previousState = currentState;
        
        currentState = to;
    }

    public void Die()
    {
        SetState(EnemyState.Die);
    }

    public void OnHeardNoise(Vector3 noisePos)
    {
        if (!movementView.IsSeeHuman &&
            noisePos.x != 0 && noisePos.y != 0 && noisePos.z != 0)
        {
            lastTargetPos = noisePos;
            SetState(EnemyState.WalkToTarget);
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
        targetPosition = movementView.HumanPos;
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
