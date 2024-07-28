using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the levelling system of the player
public class Level : MonoBehaviour
{
    public int exp = 0;
    public int level = 1; //set level to level 1 by default when game starts

    [SerializeField] expBar expBar;
    [SerializeField] UpgradeMenu upgradeMenu;

    private GameController gameController;

    void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }
    void Update()
    {
        if(gameController.gameActive)
        {
            expBar.UpdateExpBar(exp, expRequired);
        }

    }

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
