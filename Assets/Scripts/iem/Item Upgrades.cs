using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script is the constructor class for item upgrades
public class ItemUpgrades 
{
    public string itemID {get;}
    public int level {get;}
    public string upgradeDesc {get;}

    public float itemValue {get; set;}

    public string effectTime {get; set;}

    public string effectCooldown {get; set;}

    public ItemUpgrades(string itemID, int level, string upgradeDesc, float itemValue, string effectTime, string effectCooldown)
    {
        this.itemID = itemID;
        this.level = level;
        this.upgradeDesc = upgradeDesc;
        this.itemValue = itemValue;
        this.effectTime = effectTime;
        this.effectCooldown = effectCooldown;
    }
}
