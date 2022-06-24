using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private ZombieAnimations animations;

    private EnemyInfo info;
    private bool canAttack = true;

    public EnemyInfo Info { get => info; 
        set
        {
            info = value;
        }
    }

    public bool CheckAttackDistance(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= info.AttackDistance;
    }

    public void TryAttack(Transform target)
    {
        if (canAttack)
        {
            Health targetHealth = target.GetComponent<Health>();
            animations.SetAttack();

            if (targetHealth)
            {
                targetHealth.Damage(info.Damage);
                //Debug.Log("Attack " + target.gameObject.name);
            }

            canAttack = false;
            
            Invoke(nameof(ResetAttack), info.TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
