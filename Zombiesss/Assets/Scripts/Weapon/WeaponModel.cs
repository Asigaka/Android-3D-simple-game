using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    [SerializeField] private WeaponItemInfo weaponInfo;
    [SerializeField] private Transform firePoint;

    public WeaponItemInfo WeaponInfo { get => weaponInfo; }
    public bool CanShot { get; set; }
    public Transform FirePoint { get => firePoint; }

    private void Start()
    {
        CanShot = true;
    }

    public void PlayVisual()
    {

    }
}
