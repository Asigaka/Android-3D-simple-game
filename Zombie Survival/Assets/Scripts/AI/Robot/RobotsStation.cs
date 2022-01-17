using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsStation : MonoBehaviour
{
    [SerializeField] private GameObject robotPrefab;
    [SerializeField] private Transform robotsFixedPosition;
    [SerializeField] private float robotsTurnOnTime = 3;

    private RobotMovementController robotController;

    private void Start()
    {
        robotController = Instantiate(robotPrefab, robotsFixedPosition.position, robotsFixedPosition.rotation).GetComponent<RobotMovementController>();
        robotController.SetState(RobotMovementController.RobotState.OnStation);

        if (robotController == null)
            Debug.Log("” станции отсутствует контроллер робота!");
    }

    public void TurnOnRobot()
    {
        StartCoroutine(RobotsPrepareToTurnOn());
    }

    private IEnumerator RobotsPrepareToTurnOn()
    {
        yield return new WaitForSeconds(robotsTurnOnTime);
        robotController.SetState(RobotMovementController.RobotState.Idle);
    }
}
