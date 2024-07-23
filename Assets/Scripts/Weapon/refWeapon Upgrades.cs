using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the reference class for weapon upgrades
// It takes the data pulled from the csv in datamanager script and then assigns the data to the various fields
// It is then used in weaponUpgrades script
public class refWeaponUpgrades
{
    public string refID;
    public int level;
    public int projectileCount;
    public float dmgMultiplier;
    public float fireRate;

    public string upgradeDesc;
}
