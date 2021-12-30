using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void DamageObject(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            OnBreak();
    }

    public void OnBreak()
    {
        Destroy(gameObject);
    }
}
