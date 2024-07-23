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

    public ItemUpgrades(string itemID, int level, string upgradeDesc)
    {
        this.itemID = itemID;
        this.level = level;
        this.upgradeDesc = upgradeDesc;
    }
}
