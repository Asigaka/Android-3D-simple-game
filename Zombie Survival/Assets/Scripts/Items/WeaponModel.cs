using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    public ItemInfo Info;
    public ItemWeaponInfo WeaponInfo;
    public Transform FirePoint;
    public GameObject ShootImpact;

    [Space(7)]
    public int AmmoInMagazinAmount;
    public bool WeaponReloaded;
    public bool WeaponRateOfFired;
    public float LocalRateOfFireTimer;
}
