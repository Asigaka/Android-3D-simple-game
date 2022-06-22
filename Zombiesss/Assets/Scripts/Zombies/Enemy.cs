using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyCombat combat;

    [Space]
    [SerializeField] private bool seePlayerOnStart = true;

    private void Start()
    {
        if (seePlayerOnStart)
            SetPlayerTarget();
    }

    public void SetPlayerTarget()
    {
        movement.MoveTarget = Session.Instance.Player.transform;
    }
}
