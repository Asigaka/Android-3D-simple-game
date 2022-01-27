using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsPlayer, whatIsGround;
    [SerializeField] private FieldOfView view;
    //[SerializeField] private EnemyAnimations anim;

    [Header("Patrolling")]
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;

    [Header("States")]
    [SerializeField] private float attackRange;
    [SerializeField] private bool playerInAttackRange;

    private void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

       // if (!view.IsSeeHuman && !playerInAttackRange) Patrolling();
        if (view.IsSeeHuman && !playerInAttackRange) ChasePlayer();
        if (view.IsSeeHuman && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        //anim.SetWalk(true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);
          Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

        //anim.SetWalk(true);
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(view.HumanPos);
       // anim.SetWalk(true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(view.HumanPos);
        transform.LookAt(new Vector3(view.HumanPos.x, transform.position.y, view.HumanPos.z));

        if (!alreadyAttacked)
        {
            //Rigidbody rb = Instantiate(pro);
            alreadyAttacked = true;
            //anim.AttackTrigger();
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
