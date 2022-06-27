using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth;

    [HideInInspector] public UnityEvent onDamage;
    [HideInInspector] public UnityEvent onDie;

    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; 
        set
        {
            maxHealth = value;
            CurrentHealth = maxHealth;
        }
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        onDamage.Invoke();

        if (CurrentHealth <= 0)
            onDie.Invoke();
    }
}
