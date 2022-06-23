using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfo enemyInfo;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyCombat combat;
    [SerializeField] private Health health;

    [Space]
    [SerializeField] private bool seePlayerOnStart = true;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private LayerMask checkLayer;
    [SerializeField] private float checkDistance = 2;

    [SerializeField] private Transform target;
    [SerializeField] private bool seePlayerLate;

    public EnemyInfo EnemyInfo { get => enemyInfo; }

    private void Start()
    {
        movement.Info = enemyInfo;
        combat.Info = enemyInfo;
        health.MaxHealth = enemyInfo.MaxHealth;
        GetComponent<CapsuleCollider>().radius = enemyInfo.AttackDistance;

        seePlayerLate = seePlayerOnStart;

        if (seePlayerOnStart)
            SetPlayerTarget();
    }

    public void SetPlayerTarget()
    {
        target = Session.Instance.Player.transform;
        seePlayerLate = true;
    }

    private void Update()
    {
        CheckTargetForward();

        if (target)
        {
            movement.MoveToTarget(target);
            movement.Agent.enabled = true;

            if (combat.CheckAttackDistance(target))
            {
                movement.Agent.enabled = false;
                combat.TryAttack(target);
            }
        }
        else if (target == null && seePlayerLate && Session.Instance.Player)
        {
            target = Session.Instance.Player.transform;
            movement.Agent.enabled = true;
        }
    }

    private void CheckTargetForward()
    {
        Ray ray = new Ray(checkPoint.position, checkPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, checkDistance, checkLayer))
        {
            target = hit.transform;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer) return;

        Health health = other.GetComponent<Health>();

        if (health)
        {
            target = health.transform;
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, checkPoint.forward * checkDistance);
    }
}
