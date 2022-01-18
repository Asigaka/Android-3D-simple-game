using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private Camera fpsCam;
    [SerializeField] private LayerMask enemyLayer;

    [Space(7)]
    [SerializeField] private ItemWeaponInfo currentWeapon;
    [SerializeField] private float localReloadTimer;
    [SerializeField] private float localRateOfFireTimer;
    [SerializeField] private int ammoInMagazinAmount;
    [SerializeField] private int ammoInInventoryAmount;
    [SerializeField] private bool weaponReloaded;
    [SerializeField] private bool weaponRateOfFired;

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

    public void OnEquipWeapon(ItemWeaponInfo equipedWeapon)
    {
        currentWeapon = equipedWeapon;
        CheckAmmoInInventory();
        combatUI.TurnOnUI();
        weaponRateOfFired = true;
    }

    public void OnTakeOffWeapon()
    {
        currentWeapon = null;
        combatUI.TurnOffUI();
    }

    private void Update()
    {
        if (currentWeapon != null && (ammoInInventoryAmount > 0 || ammoInMagazinAmount > 0))
        {
            if (!weaponReloaded)
                ReloadTime();
            if (!weaponRateOfFired)
                RateOfFireTime();

            PlayerEnemyCheck();
        }
    }

    private void PlayerEnemyCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, currentWeapon.Range, enemyLayer))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * currentWeapon.ImpactForce);
            }

            combatUI.TurnOnCrosshair();

            if (weaponRateOfFired && weaponReloaded)
                Shoot();
        }
        else
        {
            combatUI.TurnOffCrosshair();
        }
    }

    private void FillMagazine()
    {
        Debug.Log("Заполняю магазин");

        if (currentWeapon.MagazineSize >= ammoInInventoryAmount)
        {
            ammoInMagazinAmount = ammoInInventoryAmount;
            PlayerInventory.Instance.RemoveItemFromInventory(currentWeapon.AmmoInfo, ammoInInventoryAmount);
        }
        else
        {
            int ammoInterim = ammoInInventoryAmount - currentWeapon.MagazineSize;
            PlayerInventory.Instance.RemoveItemFromInventory(currentWeapon.AmmoInfo, ammoInInventoryAmount - ammoInterim);
            ammoInMagazinAmount = currentWeapon.MagazineSize;
        }

        CheckAmmoInInventory();
    }

    private void Shoot()
    {
        if (ammoInMagazinAmount > 0 && weaponReloaded)
        {
            Debug.Log("Выстрел");
            weaponRateOfFired = false;
            ammoInMagazinAmount--;
            CheckAmmoInInventory();
        }

        if (ammoInMagazinAmount <= 0)
            weaponReloaded = false;
    }

    public void CheckAmmoInInventory()
    {
        if (currentWeapon != null)
        {
            ammoInInventoryAmount = PlayerInventory.Instance.GetItemAmount(currentWeapon.AmmoInfo);
            combatUI.UpdateUI(ammoInMagazinAmount, ammoInInventoryAmount);
        }
        Debug.Log("Боеприпасов в инвентаре: " + ammoInInventoryAmount);
    }

    private void RateOfFireTime()
    {
        if (localRateOfFireTimer <= 0 && !weaponRateOfFired)
        {
            localRateOfFireTimer = currentWeapon.RateOfFire;
            weaponRateOfFired = true;
        }
        else
        {
            localRateOfFireTimer -= Time.deltaTime;
            weaponRateOfFired = false;
        }
    }

    private void ReloadTime()
    {
        if (localReloadTimer <= 0 && !weaponReloaded)
        {
            if (ammoInMagazinAmount == 0 && ammoInInventoryAmount > 0)
            {
                FillMagazine();
            }

            if (ammoInMagazinAmount > 0)
            {
                localReloadTimer = currentWeapon.ReloadTime;
                weaponReloaded = true;
            }
        }
        else
        {
            localReloadTimer -= Time.deltaTime;
            weaponReloaded = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentWeapon != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * currentWeapon.Range);
        }
    }
}
