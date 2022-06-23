using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponModel selectedWeapon;
    [SerializeField] private LayerMask enemyLayer;

    public void TryShot()
    {
        if (selectedWeapon && selectedWeapon.CanShot)
        {
            selectedWeapon.CanShot = false;
            selectedWeapon.PlayVisual();

            Ray ray = new Ray(selectedWeapon.FirePoint.position, selectedWeapon.FirePoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, selectedWeapon.WeaponInfo.Range, enemyLayer))
            {
                if (hit.collider)
                {
                    Health health = hit.collider.GetComponent<Health>();

                    if (!health)
                        health = hit.collider.GetComponentInParent<Health>();

                    if (health)
                    {
                        health.Damage(selectedWeapon.WeaponInfo.Damage);
                    }
                }
            }

            Invoke(nameof(ResetShot), selectedWeapon.WeaponInfo.TimeBetweenShots);
        }
    }

    private void ResetShot()
    {
        selectedWeapon.CanShot = true;
    }

    private void OnDrawGizmos()
    {
        if (selectedWeapon)
        {
            Gizmos.DrawRay(selectedWeapon.FirePoint.position, selectedWeapon.FirePoint.forward * selectedWeapon.WeaponInfo.Range);
        }
    }
}
