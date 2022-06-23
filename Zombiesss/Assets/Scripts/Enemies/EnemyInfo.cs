using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy")]
public class EnemyInfo : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;

    [Header("Combat")]
    [SerializeField] private float attackDistance;
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttacks;

    public float MaxHealth { get => maxHealth; }
    public float Speed { get => speed; }
    public float AttackDistance { get => attackDistance; }
    public float Damage { get => damage; }
    public float TimeBetweenAttacks { get => timeBetweenAttacks; }
}
