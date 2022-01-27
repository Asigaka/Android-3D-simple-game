using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, ICreature
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private FieldOfView view;
    [SerializeField] private EnemyAnimations anim;

    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private bool attackIsReady;
    [SerializeField] private bool targetIsHuman;

    private Vector3 targetPos;
    private Vector3 lastTargetPos;

    private void Start()
    {
        attackIsReady = true;
    }

    private void Update()
    {
        anim.SetWalk(agent.velocity.magnitude != 0);
        view.CheckHumanAround();

        if (view.IsSeeHuman)
        {
            OnSeePlayer();
        }

        targetIsHuman = view.IsSeeHuman;

        if (CanAttack() && attackIsReady)
        {
            transform.LookAt(new Vector3(view.HumanPos.x, transform.position.y, view.HumanPos.z));
            Attack();
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private bool CanAttack()
    {
        if (targetIsHuman)
        {
            Vector3 distanceToHuman = transform.position - targetPos;

            if (distanceToHuman.magnitude < distanceToAttack)
            {
                return true;
            }
        }

        return false;
    }

    private void Attack()
    {
        anim.AttackTrigger();
        attackIsReady = false;
        PlayerHealth.Instance.DamagePlayer(damage);
    }

    private void ResetAttack()
    {
        attackIsReady = true;
    }

    public void Die()
    {
        agent.enabled = false;
        anim.DieTrigger();
    }

    private void WalkToLastTarget()
    {
        if (lastTargetPos.x != 0 && lastTargetPos.y != 0 && lastTargetPos.z != 0)
        {
            agent.SetDestination(lastTargetPos);
        }
    }

    public void OnHeardNoise(Vector3 noisePos)
    {
        if (!view.IsSeeHuman && (noisePos.x != 0 && noisePos.y != 0 && noisePos.z != 0))
        {
            lastTargetPos = noisePos;
        }
    }

    public void OnMetObstacle()
    {
        throw new System.NotImplementedException();
    }

    public void OnSeePlayer()
    {
        targetPos = view.HumanPos;
        agent.SetDestination(targetPos);
        if (lastTargetPos != targetPos)
            lastTargetPos = targetPos;
    }
}
