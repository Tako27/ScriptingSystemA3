using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the levelling system of the player
public class Level : MonoBehaviour
{
    int exp = 0;
    int level = 1; //set level to llevel 1 by default when game starts

    [SerializeField] expBar expBar;
    [SerializeField] UpgradeMenu upgradeMenu;

    int expRequired //this is to get the amount of exp required to level up for each level
    {
        get
        {
            return level * 1000;
        }
    }
    public void AddExperience(int amount) ///this handles the addition of experience when player picks up a exp drop
    {
        exp += amount;
        LevelUp();
        expBar.UpdateExpBar(exp, expRequired);
    }

    public void LevelUp() //this handles the incrementation of levels
    {
        
        if(exp >= expRequired)
        {
            upgradeMenu.OpenUpgradeMenu();
            exp -= expRequired;
            level++;

        }
    }

}
