using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    [SerializeField] private LayerMask enemyLayer;

    [Space(7)]
    [SerializeField] private WeaponModel curWeapon;
    [SerializeField] private float localReloadTimer;
    [SerializeField] private int ammoInInventoryAmount;

    private HUDScreen hud;

    private void Start()
    {
        hud = UIManager.Instance.HUD; 
    }

    public void EquipWeapon(WeaponModel model)
    {
        if (model.WeaponInfo != null)
        {
            curWeapon = model;
            CheckAmmoInInventory();
            //combatUI.TurnOnUI();
            curWeapon.WeaponRateOfFired = true;
        }
    }

    public void OnTakeOffWeapon()
    {
        curWeapon = null;
        //combatUI.TurnOffUI();
    }

    private void Update()
    {
        if (curWeapon != null && (ammoInInventoryAmount > 0 || curWeapon.AmmoInMagazinAmount > 0))
        {
            if (!curWeapon.WeaponReloaded)
                ReloadTime();
            if (!curWeapon.WeaponRateOfFired)
                RateOfFireTime();

            PlayerEnemyCheck();
        }
    }

    private void PlayerEnemyCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, curWeapon.WeaponInfo.Range, enemyLayer))
        {
            if (hit.rigidbody != null)
            {
              // hit.rigidbody.AddForce(-hit.normal * currentWeapon.ImpactForce);
            }

            //combatUI.TurnOnCrosshair();

            Shoot(hit.collider.GetComponent<Health>());
        }
        else
        {
            //combatUI.TurnOffCrosshair();
        }
    }

    private void FillMagazine()
    {
        Debug.Log("Заполняю магазин");

        curWeapon.AmmoInMagazinAmount = ItemsHander.RemoveItemInPlayer(curWeapon.Info, curWeapon.AmmoInMagazinAmount);

        CheckAmmoInInventory();
    }

    private void Shoot(Health enemyHealth)
    {
        if (curWeapon.AmmoInMagazinAmount > 0 && curWeapon.WeaponReloaded && curWeapon.WeaponRateOfFired)
        {
            curWeapon.WeaponRateOfFired = false;
            curWeapon.AmmoInMagazinAmount--;
            CheckAmmoInInventory();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(curWeapon.WeaponInfo.Damage);
            }

            GameObject impact = Instantiate(curWeapon.ShootImpact, curWeapon.FirePoint);
            Destroy(impact, 0.3f);
        }

        if (curWeapon.AmmoInMagazinAmount <= 0)
            curWeapon.WeaponReloaded = false;
    }

    public void CheckAmmoInInventory()
    {
        if (curWeapon != null)
        {
            ammoInInventoryAmount = ItemsHander.GetItemAmountInPlayer(curWeapon.Info);
            //combatUI.UpdateUI(curWeapon.AmmoInMagazinAmount, ammoInInventoryAmount);
        }
    }

    private void RateOfFireTime()
    {
        if (curWeapon.LocalRateOfFireTimer <= 0 && !curWeapon.WeaponRateOfFired)
        {
            curWeapon.LocalRateOfFireTimer = curWeapon.WeaponInfo.RateOfFire;
            curWeapon.WeaponRateOfFired = true;
        }
        else
        {
            curWeapon.LocalRateOfFireTimer -= Time.deltaTime;
            curWeapon.WeaponRateOfFired = false;
        }
    }

    private void ReloadTime()
    {
        if (localReloadTimer <= 0 && !curWeapon.WeaponReloaded)
        {
            if (curWeapon.AmmoInMagazinAmount == 0 && ammoInInventoryAmount > 0)
            {
                FillMagazine();
            }

            if (curWeapon.AmmoInMagazinAmount > 0)
            {
                localReloadTimer = curWeapon.WeaponInfo.ReloadTime;
                curWeapon.WeaponReloaded = true;
            }
        }
        else
        {
            localReloadTimer -= Time.deltaTime;
            curWeapon.WeaponReloaded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (curWeapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * curWeapon.WeaponInfo.Range);
        }
    }
}
