using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScreen : UIScreen
{
    [Header("Weapon")]
    [SerializeField] private GameObject weaponAmmo;
    [SerializeField] private TextMeshProUGUI ammoAmountLabel;

    [Header("Interactions")]
    [SerializeField] private TextMeshProUGUI interactionNameLabel;

    public void SetInteractionName(string interactionName) => interactionNameLabel.text = interactionName;

    public void SelectedWeaponUI(WeaponModel weapon)
    {
        if (weapon)
        {
            weaponAmmo.SetActive(true);
        }
        else
        {
            weaponAmmo.SetActive(false);
        }
    }
}
