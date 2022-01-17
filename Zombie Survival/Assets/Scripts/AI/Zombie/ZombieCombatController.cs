using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatController : MonoBehaviour
{
    [SerializeField] private float timeToAttack;
    [SerializeField] private float objectDamage;

    [Space(7)]
    [SerializeField] private LayerMask doorLayer;

    private float localTimeToAttack;

    private void Start()
    {
        localTimeToAttack = timeToAttack;
    }

    public void Attack(ObjectHealth objectHealth)
    {
        if (ReadyToAttack() && objectHealth)
        {
            objectHealth.DamageObject(objectDamage);
        }
    }

    public ObjectHealth CheckObjectForward()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, doorLayer))
        {
            return hit.collider.GetComponent<ObjectHealth>();
        }

        return null;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 1);
    }
}
