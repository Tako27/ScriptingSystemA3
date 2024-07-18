using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weaponInventory = new List<Weapon>();
    public List<item> itemInventory = new List<item>();

    public bool isUpgrading;

    private WeaponUpgrades nextLevelWeapon;


    void Start()
    {
        //when the game starts, get the type of character that the player has selected
        //based on the selected character, add the assigned default weapon to inventory
    }

    public void InitializaWeaponStats(Weapon weapon)
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

    public void InitializaItemStats(item item)
    {
        List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
        ItemUpgrades levelOneItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //find level 1 data for item

        //setting all necessary stats
        item.initiallevel = levelOneItem.level;
        item.basicDesc = levelOneItem.upgradeDesc;
    }
    public void AddWeaponToInventory(Weapon weapon)
    {   

        Weapon weaponInInventory = weaponInventory.Find(w => w.id == weapon.id);  //check inventory for weapon
        if(weaponInInventory != null) //if the weapon is already in inventory, upgrade the weapon to the next level
        {
            UpgradeWeapon(weaponInInventory);
            Debug.Log("Now" + weaponInInventory.name + "is level:" + weaponInInventory.initialLevel);
        }
        else
        {
            //get initial weapon stats --> weapon stats at level 1

            InitializaWeaponStats(weapon);
            weaponInventory.Add(weapon);

        }
        
        
    }

    public void AddItemToInventory(item item)
    {
        item itemInInventory = itemInventory.Find(w => w.id == item.id);  //check inventory for item
        if(itemInInventory != null) //if the weapon is already in inventory, upgrade the weapon to the next level
        {
            UpgradeItem(itemInInventory);
        }
        else
        {
            InitializaItemStats(item);
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

    public void UpgradeWeapon(Weapon weapon)
    {
        List<WeaponUpgrades> upgradeList = Game.GetWeaponUpgradesList().FindAll(upgrade => upgrade.refID == weapon.id); //this is to find all levels for this specific weapon
        nextLevelWeapon = upgradeList.Find(upgrade => upgrade.level == weapon.initialLevel + 1);

        if(nextLevelWeapon != null)
        {
            weapon.initialLevel = nextLevelWeapon.level;

            weapon.projectileCount = nextLevelWeapon.projectileCount;
            weapon.dmgMultiplier = nextLevelWeapon.dmgMultiplier;
            weapon.fireRate = nextLevelWeapon.fireRate;
            weapon.basicDesc = nextLevelWeapon.upgradeDesc;
        }
    }

    public void UpgradeItem(item item)
    {
        List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id); //this is to find all levels for this specific item
        ItemUpgrades nextLevel = upgradeList.Find(upgrade => upgrade.level == item.initiallevel + 1);

        if(nextLevel != null)
        {
            item.initiallevel = nextLevel.level;
            item.basicDesc = nextLevel.upgradeDesc;
        }
    }

    public void ReplaceWeaponInInventory(int index, Weapon weapon)
    {
        InitializaWeaponStats(weapon);
        Debug.Log("replacing:" + weaponInventory[index].name);
        weaponInventory[index] = weapon;
        Debug.Log("now inventory has:" + weaponInventory[index].name);
        Debug.Log("slot1:" + weaponInventory[0].name);
        Debug.Log("slot2:" + weaponInventory[1].name);
        Debug.Log("slot3:" + weaponInventory[2].name);
        
        
    }

    public void ReplaceItemInInvetory(int index, item item)
    {
        InitializaItemStats(item);
        itemInventory[index] = item;
    }
}
