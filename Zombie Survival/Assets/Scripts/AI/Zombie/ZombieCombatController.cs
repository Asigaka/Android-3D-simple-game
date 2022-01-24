using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCombatController : MonoBehaviour
{
    [SerializeField] private FieldOfView combatView;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float playerDamage;

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

    public void Attack()
    {
        if (ReadyToAttack())
        {
            PlayerHealth.Instance.DamagePlayer(playerDamage);
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
