using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Weapon")]
public class WeaponItemInfo : ItemInfo
{
    [Space]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float timeBetweenShots;

    public float Damage { get => damage; }
    public float Range { get => range; }
    public float TimeBetweenShots { get => timeBetweenShots; }
}
