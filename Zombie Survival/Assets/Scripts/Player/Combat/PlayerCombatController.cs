using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    [SerializeField] private LayerMask enemyLayer;

    [Space(7)]
    [SerializeField] private WeaponModel curWeapon;
    [SerializeField] private float localReloadTimer;
    [SerializeField] private int ammoInInventoryAmount;

    private PlayerCombatUI combatUI;

    public static PlayerCombatController Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        combatUI = PlayerCombatUI.Instance; 
    }

    public void EquipWeapon(WeaponModel model)
    {
        curWeapon = model;
        CheckAmmoInInventory();
        combatUI.TurnOnUI();
        curWeapon.WeaponRateOfFired = true;
    }

    public void OnTakeOffWeapon()
    {
        curWeapon = null;
        combatUI.TurnOffUI();
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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, curWeapon.Info.Range, enemyLayer))
        {
            if (hit.rigidbody != null)
            {
              // hit.rigidbody.AddForce(-hit.normal * currentWeapon.ImpactForce);
            }

            combatUI.TurnOnCrosshair();

            Shoot(hit.collider.GetComponent<EnemyHealth>());
        }
        else
        {
            combatUI.TurnOffCrosshair();
        }
    }

    private void FillMagazine()
    {
        //Debug.Log("Заполняю магазин");

        if (curWeapon.Info.MagazineSize >= ammoInInventoryAmount)
        {
            curWeapon.AmmoInMagazinAmount = ammoInInventoryAmount;
            PlayerInventory.Instance.RemoveItemFromInventory(curWeapon.Info.AmmoInfo, ammoInInventoryAmount);
        }
        else
        {
            int ammoInterim = ammoInInventoryAmount - curWeapon.Info.MagazineSize;
            PlayerInventory.Instance.RemoveItemFromInventory(curWeapon.Info.AmmoInfo, ammoInInventoryAmount - ammoInterim);
            curWeapon.AmmoInMagazinAmount = curWeapon.Info.MagazineSize;
        }

        CheckAmmoInInventory();
    }

    private void Shoot(EnemyHealth enemyHealth)
    {
        if (curWeapon.AmmoInMagazinAmount > 0 && curWeapon.WeaponReloaded && curWeapon.WeaponRateOfFired)
        {
            curWeapon.WeaponRateOfFired = false;
            curWeapon.AmmoInMagazinAmount--;
            CheckAmmoInInventory();
            if (enemyHealth != null)
            {
                enemyHealth.DamageEnemy(curWeapon.Info.Damage);
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
            ammoInInventoryAmount = PlayerInventory.Instance.GetItemAmount(curWeapon.Info.AmmoInfo);
            combatUI.UpdateUI(curWeapon.AmmoInMagazinAmount, ammoInInventoryAmount);
        }
        //Debug.Log("Боеприпасов в инвентаре: " + ammoInInventoryAmount);
    }

    private void RateOfFireTime()
    {
        if (curWeapon.LocalRateOfFireTimer <= 0 && !curWeapon.WeaponRateOfFired)
        {
            curWeapon.LocalRateOfFireTimer = curWeapon.Info.RateOfFire;
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
                localReloadTimer = curWeapon.Info.ReloadTime;
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
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * curWeapon.Info.Range);
        }
    }
}
