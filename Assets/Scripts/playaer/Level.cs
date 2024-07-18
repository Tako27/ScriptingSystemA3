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

    void Start()
    {

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
            upgradeMenu.OpenUpgradeMenu();
            exp -= expRequired;
            level++;

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

    
}
