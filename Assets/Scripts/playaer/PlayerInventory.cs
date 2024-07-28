using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script handles the player inventory, as well as addition or replacement of weapons or items.
// This script also handles the activation of the different weapons controller.

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weaponInventory = new List<Weapon>();
    public List<item> itemInventory = new List<item>();

    public bool isUpgrading;

    private WeaponUpgrades nextLevelWeapon;

    public List<WeaponController> weaponControllerList;

    public void InitializaWeaponStats(Weapon weapon) //this is to handle initialization of weapons 
    {
        List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == weapon.id); //this is to find all levels for this specific weapon
        WeaponUpgrades levelOneWeapon = upgradeList.Find(upgrade => upgrade.level == weapon.initialLevel); //find level 1 data for this weapon

        //setting all necessary stats 
        weapon.initialLevel = levelOneWeapon.level;
        weapon.projectileCount = levelOneWeapon.projectileCount;
        weapon.dmgMultiplier = levelOneWeapon.dmgMultiplier;
        weapon.fireRate = levelOneWeapon.fireRate;
        weapon.basicDesc = levelOneWeapon.upgradeDesc;
    }

    public void InitializaItemStats(item item) //handle initialization of items
    {
        List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
        ItemUpgrades levelOneItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //find level 1 data for item

        //setting all necessary stats
        item.initiallevel = levelOneItem.level;
        item.basicDesc = levelOneItem.upgradeDesc;
    }
    public void AddWeaponToInventory(Weapon weapon) //handles additon of weapons
    {   

        Weapon weaponInInventory = weaponInventory.Find(w => w.id == weapon.id);  //check inventory for weapon
        if(weaponInInventory != null) //if the weapon is already in inventory, upgrade the weapon to the next level
        {
            UpgradeWeapon(weaponInInventory);
            Debug.Log("Now" + weaponInInventory.name + "is level:" + weaponInInventory.initialLevel);
        }
        else //weapon is not in inventory
        {
            //get initial weapon stats --> weapon stats at level 1

            InitializaWeaponStats(weapon);
            weaponInventory.Add(weapon);
            EnableWeapon(weapon);
        }      
    }

    public void AddItemToInventory(item item) //handles addition of items
    {
        item itemInInventory = itemInventory.Find(w => w.id == item.id);  //check inventory for item
        if(itemInInventory != null) //if the weapon is already in inventory, upgrade the weapon to the next level
        {
            UpgradeItem(itemInInventory);
        }
        else //item is not in inventory
        {
            //get initial item stats --> items stats at level 1
            InitializaItemStats(item);
            //add it to inventory
            itemInventory.Add(item);
        }
        
    }

    public List<Weapon> GetWeaponInventory()
    {
        return weaponInventory;
    }

    public List<item> GetItemInventory()
    {
        return itemInventory;
    }

    public void UpgradeWeapon(Weapon weapon) //handles weapon upgrading 
    {
        List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == weapon.id); //this is to find all levels for this specific weapon
        nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == weapon.initialLevel + 1); //find the next level from the previous list of weapon upgrades
        
        //if the weapon is not already max level, set selected weapon stats to that of the next level
        if(nextLevelWeapon != null) 
        {
            weapon.initialLevel = nextLevelWeapon.level;

            weapon.projectileCount = nextLevelWeapon.projectileCount;
            weapon.dmgMultiplier = nextLevelWeapon.dmgMultiplier;
            weapon.fireRate = nextLevelWeapon.fireRate;
            weapon.basicDesc = nextLevelWeapon.upgradeDesc;
            weapon.weaponRangeMultiplier = nextLevelWeapon.weaponRangeMultiplier;
        }
    }

    public void UpgradeItem(item item) //handle item upgrading
    {
        List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
        ItemUpgrades nextLevel = upgradeList.Find(upgrade => upgrade.level == item.initiallevel + 1); //find the next levell from the previous list of item upgrades

        //if item is not already max level, set selected item stats to that of the next level
        if(nextLevel != null)
        {
            item.initiallevel = nextLevel.level;
            item.basicDesc = nextLevel.upgradeDesc;
        }
    }

    //in the game, player has 2 types of inventory: weapon and item
    //both inventory acts independently of the other
    //maximum number of slots for each inventory is 3, and if player selects a weapon or item that is not in inventory when it is full, player will be prompted to select aa weapon or item in inventory to replace
    public void ReplaceWeaponInInventory(int index, Weapon weapon)  //handle weapon replacement
    {
        InitializaWeaponStats(weapon); //get the default state of the new weapon from upgrades menu --> level 1
        weaponInventory[index] = weapon; //based on index of selected inventory slot, replace the weapon in the selected slot with the new weapon
    }

    public void ReplaceItemInInvetory(int index, item item) //handle item replacement
    {
        InitializaItemStats(item); //get default state of the new item from uupgrades menu --> level 1
        itemInventory[index] = item; //based on index of selected inventory slot, repplace item in selected slot with new item
    }


    #region Weapon Controller Section

    public void EnableWeapon(Weapon weaponRef)
    {
        int weaponID = int.Parse(weaponRef.id.Substring(1)) - 1;

        try
        {
            weaponControllerList[weaponID].InitializeWeapon(weaponRef);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Weapon controller not found");


            weaponControllerList[7].InitializeWeapon(weaponRef);
        }
    }

    #endregion
}
