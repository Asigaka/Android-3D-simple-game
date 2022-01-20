using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombatUI : MonoBehaviour
{
    [SerializeField] private GameObject weaponUI;
    [SerializeField] private GameObject weaponCrosshair;
    [SerializeField] private TextMeshProUGUI ammoText;

    public static PlayerCombatUI Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void Start()
    {
        TurnOffUI();
    }

    public void TurnOnUI()
    {
        weaponUI.SetActive(true);
        weaponCrosshair.SetActive(true);
    }

    public void TurnOffUI()
    {
        weaponUI.SetActive(false);
        weaponCrosshair.SetActive(false);
    }

    public void TurnOnCrosshair()
    {
        weaponCrosshair.SetActive(true);
    }

    public void TurnOffCrosshair()
    {
        weaponCrosshair.SetActive(false);
    }

    public void UpdateUI(int ammoInMagazine, int ammoInInventory)
    {
        ammoText.text = ammoInMagazine + "/" + ammoInInventory;
    }
}
