using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;

    public ItemWeaponInfo CurrentWeapon;

    public static PlayerCombatController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (CurrentWeapon != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, CurrentWeapon.Range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * CurrentWeapon.ImpactForce);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (CurrentWeapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * CurrentWeapon.Range);
        }
    }
}
