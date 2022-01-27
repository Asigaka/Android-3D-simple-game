using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    [SerializeField] private EnemyAnimations anim;
    [SerializeField] private FieldOfView combatView;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float playerDamage;
    [SerializeField] private float attackDistance;

    private float localTimeToAttack;

    public FieldOfView CombatView { get => combatView; }

    private void Start()
    {
        localTimeToAttack = timeToAttack;
    }

    private void Update()
    {
        if (!ReadyToAttack())
            ReadyToAttack();
    }

    public  bool CanAttack()
    {
        Vector3 distanceToTarget = transform.position - transform.position;
        return distanceToTarget.magnitude < attackDistance;                     
    }

    public void Attack()
    {
        if (ReadyToAttack())
        {
            PlayerHealth.Instance.DamagePlayer(playerDamage);
            anim.AttackTrigger();
        }
    }

    private bool ReadyToAttack()
    {
        if (localTimeToAttack <= 0)
        {
            localTimeToAttack = timeToAttack;
            return true;
        }
        else
        {
            localTimeToAttack -= Time.deltaTime;
            return false;
        }
    }
}
