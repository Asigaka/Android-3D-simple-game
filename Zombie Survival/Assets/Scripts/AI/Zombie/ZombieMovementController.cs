using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovementController : MonoBehaviour, ICreature
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ZombieAnimations anim;
    [SerializeField] private ZombieCombatController combatController;
    [SerializeField] private FieldOfView movementView;

    private Vector3 targetPosition;
    public Vector3 lastTargetPos;

    private enum ZombieState { Idle, WalkToTarget, PursueTarget, Attack}

    [SerializeField] private ZombieState currentState = ZombieState.Idle;
    private ZombieState previousState;

    private void Update()
    {
        ZombieStateMachine();
    }

    private void ZombieStateMachine()
    {
        movementView.CheckHumanAround();
        
        switch (currentState)
        {
            case ZombieState.Idle:
                anim.SetWalk(false);
                if (movementView.IsSeeHuman)
                {
                    SetState(ZombieState.PursueTarget);
                }
                else if (!movementView.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    SetState(ZombieState.WalkToTarget);
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
            case ZombieState.WalkToTarget:
                anim.SetWalk(true);
                if (combatController.CombatView.IsSeeHuman)
                {
                    SetState(ZombieState.Attack);
                }

                WalkToLastTarget();
                if (movementView.IsSeeHuman)
                {
                    SetState(ZombieState.PursueTarget);
                }
                else if (!movementView.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    return;
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
            case ZombieState.PursueTarget:
                anim.SetWalk(true);
                if (agent.stoppingDistance >= agent.remainingDistance)
                {
                    SetState(ZombieState.Attack);
                }

                Debug.Log(agent.stoppingDistance);
                Debug.Log(agent.remainingDistance);

                if (movementView.IsSeeHuman)
                {
                    OnSeePlayer();
                }
                else
                {
                    SetState(ZombieState.Idle);
                }
                break;
            case ZombieState.Attack:
                anim.SetRandomAttackAnim();
                combatController.Attack();
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
        if (!movementView.IsSeeHuman &&
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
