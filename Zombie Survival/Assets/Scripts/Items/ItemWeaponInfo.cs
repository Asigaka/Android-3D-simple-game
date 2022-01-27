using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/WeaponItem", fileName = "WeaponItem")]
public class ItemWeaponInfo : ItemInfo
{
    public int WeaponID;
    public float Damage;
    public float Range;
    //public float Spread;
    public float ReloadTime;
    public float RateOfFire;
    //public float ImpactForce;
    public float ShootingNoise;
    public ItemAmmoInfo AmmoInfo;
    public int MagazineSize;
    //public int BulletPerTap;
}
