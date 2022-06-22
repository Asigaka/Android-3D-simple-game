using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    [HideInInspector] public UnityEvent onDamage = new UnityEvent();
    [HideInInspector] public UnityEvent onDie = new UnityEvent();

    public float MaxHealth { get => maxHealth;
        set
        {
            maxHealth = value;
            currentHealth = maxHealth;
        }
    }
    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float damage)
    {
        CurrentHealth -= damage;
        onDamage.Invoke();

        if (CurrentHealth <= 0)
        {
            onDie.Invoke();
            Destroy(gameObject);
        }
    }
}
