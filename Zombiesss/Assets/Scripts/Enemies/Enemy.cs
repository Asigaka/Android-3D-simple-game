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

    [SerializeField] private Transform target;

    public EnemyInfo EnemyInfo { get => enemyInfo; }

    private void Start()
    {
        movement.Info = enemyInfo;
        combat.Info = enemyInfo;
        health.MaxHealth = enemyInfo.MaxHealth;
        GetComponent<CapsuleCollider>().radius = enemyInfo.AttackDistance;

        if (seePlayerOnStart)
            SetPlayerTarget();
    }

    public void SetPlayerTarget()
    {
        target = Session.Instance.Player.transform;
    }

    private void Update()
    {
        if (target)
        {
            movement.MoveToTarget(target);

            if (combat.CheckAttackDistance(target))
            {
                combat.TryAttack(target);
            }
        }
        else if (target == null && seePlayerOnStart && Session.Instance.Player)
        {
            target = Session.Instance.Player.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gameObject.layer) return;

        Health health = other.GetComponent<Health>();

        if (health)
        {
            target = health.transform;
        }
    }
}
