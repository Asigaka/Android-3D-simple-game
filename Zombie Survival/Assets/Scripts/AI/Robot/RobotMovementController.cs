using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(FieldOfView))]
public class RobotMovementController : MonoBehaviour, ICreature
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light facePointLight;
    [SerializeField] private Light faceSpotLight;
   // [SerializeField] private 

    private Vector3 targetPosition;
    private Vector3 lastTargetPos;

    private FieldOfView fov;

    public enum RobotState {OnStation, Idle, WalkToTarget, PursueTarget, Attack }
    [SerializeField]
    private RobotState currentState = RobotState.Idle;
    private RobotState previousState;

    private void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        RobotStateMachine();
    }

    private void RobotStateMachine()
    {
        fov.CheckHumanAround();
        facePointLight.gameObject.SetActive(currentState != RobotState.OnStation);
        faceSpotLight.gameObject.SetActive(currentState != RobotState.OnStation);

        switch (currentState)
        {
            case RobotState.OnStation:
                break;
            case RobotState.Idle:
                facePointLight.color = Color.green;
                faceSpotLight.color = Color.green;

                if (fov.IsSeeHuman)
                {
                    SetState(RobotState.PursueTarget);
                }
                else if (!fov.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    SetState(RobotState.WalkToTarget);
                }
                else
                {
                    SetState(RobotState.Idle);
                }
                break;
            case RobotState.WalkToTarget:
                /*if (combatController.CheckObjectForward() != null)
                {
                    combatController.Attack(combatController.CheckObjectForward());
                }*/
                facePointLight.color = Color.yellow;
                faceSpotLight.color = Color.yellow;

                WalkToLastTarget();
                if (fov.IsSeeHuman)
                {
                    SetState(RobotState.PursueTarget);
                }
                else if (!fov.IsSeeHuman && agent.remainingDistance > agent.stoppingDistance)
                {
                    return;
                }
                else
                {
                    SetState(RobotState.Idle);
                }
                break;
            case RobotState.PursueTarget:
                /*if (combatController.CheckObjectForward() != null)
                {
                    combatController.Attack(combatController.CheckObjectForward());
                }*/
                facePointLight.color = Color.red;
                faceSpotLight.color = Color.red;

                if (fov.IsSeeHuman)
                {
                    OnSeePlayer();
                }
                else
                {
                    SetState(RobotState.Idle);
                }
                break;
            case RobotState.Attack:

                break;
        }
    }

    public void SetState(RobotState to)
    {
        if (previousState != currentState)
            previousState = currentState;

        currentState = to;
    }

    public void OnHeardNoise(Vector3 noisePos)
    {
        if (!fov.IsSeeHuman && noisePos.x != 0 && noisePos.y != 0 && noisePos.z != 0 && currentState != RobotState.OnStation)
        {
            lastTargetPos = noisePos;
            SetState(RobotState.WalkToTarget);
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
        targetPosition = fov.Targethealth.transform.position;
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
