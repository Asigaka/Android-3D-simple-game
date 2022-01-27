using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemyController controller;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamageEnemy(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            OnEnemyDie();
    }

    public void OnEnemyDie()
    {
        controller.Die();
    }
}
