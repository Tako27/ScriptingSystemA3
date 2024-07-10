using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Level : MonoBehaviour
{
    int exp = 0;
    int level = 1;

    [SerializeField] expBar expBar;
    [SerializeField] UpgradeMenu upgradeMenu;

    private List<Weapon> weaponUpgrades;
    private List<item> itemUpgrades;

    void Start()
    {
        weaponUpgrades = Game.GetWeaponList();
        itemUpgrades = Game.GetItemList(); 
        Debug.Log("Player is level" + level);
    }
    int expRequired
    {
        get
        {
            return level * 1000;
        }
    }
    public void AddExperience(int amount)
    {
        exp += amount;
        LevelUp();
        expBar.UpdateExpBar(exp, expRequired);
    }

    public void LevelUp()
    {
        if(exp >= expRequired)
        {
            upgradeMenu.OpenMenu();
            exp -= expRequired;
            level++;
            Debug.Log("Player levelled up! Player is level" + level);
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetExpRequired()
    {
        return expRequired;
    }

    public int GetCurrentExp()
    {
        return exp;
    }

    public List<Weapon> GetWeaponUpgrades(int count)
    {
        List<Weapon> list = new List<Weapon>();
        foreach(Weapon w in weaponUpgrades)
        {
            if(!w.isGeneric)
            {
                for(int i =0; i<count; i++)
                {
                    list.Add(weaponUpgrades[Random.Range(0,weaponUpgrades.Count)]);
                }
            }
            
            
        }
        return list;
    }

    public List<item> GeItemUpgrades(int count)
    {
        List<item> list = new List<item>();
        for(int i =0; i<count; i++)
        {
            list.Add(itemUpgrades[Random.Range(0,itemUpgrades.Count)]);
        }
        
        return list;
    }
}
