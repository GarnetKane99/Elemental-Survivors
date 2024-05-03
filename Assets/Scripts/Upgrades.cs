using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName ="Scriptable Objects/Upgrade")]
public class Upgrades : ScriptableObject
{
    public enum UpgradeTypes
    {
        pickupRadius,
        speed,
        damage,
        newWeapon,
        attackSpeed,
        bulletLifetime,
        cooldown,
        ammo,
        hitLifetime
    }

    public enum UpgradeRarity
    {
        Common,
        Uncommon,
        Rare,
        Master,
        Legendary
    }

    public UpgradeTypes upgradeType;
    public UpgradeRarity upgradeRarity;

    public AbilityData newWeapon;
    public float upgradeVal;
}
