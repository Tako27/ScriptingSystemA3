using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for weapon upgrades
public class WeaponUpgrades
{
    public string refID {get;}
    public int level {get;}
    public int projectileCount {get;}
    public float dmgMultiplier {get;}
    public float fireRate {get;}

    public string upgradeDesc {get;}

    public WeaponUpgrades(string refID, int level, int projectileCount, float dmgMultiplier, float fireRate, string upgradeDesc)
    {
        this.refID = refID;
        this.level = level;
        this.projectileCount = projectileCount;
        this.dmgMultiplier = dmgMultiplier;
        this.fireRate = fireRate;
        this.upgradeDesc = upgradeDesc;
    }


}
