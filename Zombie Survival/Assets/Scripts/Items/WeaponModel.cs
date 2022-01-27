using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    public int WeaponID;
    public ItemWeaponInfo Info;
    public Transform FirePoint;
    public GameObject ShootImpact;
    public bool RightItem;

    [Space(7)]
    public int AmmoInMagazinAmount;
    public bool WeaponReloaded;
    public bool WeaponRateOfFired;
    public float LocalRateOfFireTimer;
}
