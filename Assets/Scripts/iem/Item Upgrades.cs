using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string GetID()
    {
        return itemID;
    }

    public string GetDescription()
    {
        return upgradeDesc;
    }
}
