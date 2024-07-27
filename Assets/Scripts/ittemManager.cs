using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the effect of items

public class ittemManager : MonoBehaviour
{

     public float maxHealth;
     public float healthAdded;

    public float initialHealth;

    public float speed;

    public float initialSpeed;
    public float attack;

    public float initialAttack;
    public float expMultiplier = 1f;

    public float attackSpeed;

    public float initialAttackSpeed;
    public float pickupRange = 1f;


    public float incomingDamageMultiplier = 1f;

    private playerMovement playerMovement;
    private PlayerInventory playerInventory;

    [SerializeField] GameController gameController;

    private Player player;

    private List<ItemUpgrades> itemUpgrades;

    private bool initializedStats;

    void Start()
    {
        initializedStats = false;
        playerMovement = GetComponent<playerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        player = GetComponent<Player>();    

        itemUpgrades = Game.GetItemUpgradesList();

    }
    void Update()
    {
        
        if(gameController.gameActive) //initialize stats the moment dialogue scene closes
        {
            
            while(!initializedStats)
            {
                //store the initial stats of items --> level 1 items
                maxHealth = Game.GetChar().health;
                speed = Game.GetChar().moveSpd;
                attackSpeed = Game.GetChar().atkSpd;
                attack = Game.GetChar().atkMultiplier;


                //initialize player stats
                player.maxHealth = maxHealth;
                player.currentHealth = player.maxHealth;

                player.speedMultiplier = speed;
                player.playerAtkSpd = attackSpeed;
                player.playerAttack = attack;
                player.expGain = expMultiplier;
                player.reduceDamage = incomingDamageMultiplier;
                

                initializedStats = true;
            }
            
        }
        
    }

    public void IncreaseMaxHealth(item item) //handles health effect type items
    {
        if(item.initiallevel ==1)
        {
            player.maxHealth+=item.itemValue;
            player.currentHealth+=item.itemValue;
            healthAdded+=item.itemValue;

        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.maxHealth+=nextLevelItem.itemValue;
            player.currentHealth+=nextLevelItem.itemValue;
            healthAdded+=nextLevelItem.itemValue;

        }
    }

    public void IncreaseMoveSpeed(item item) //handles speed effect type items
    {
        if(item.initiallevel ==1)
        {
            player.speedMultiplier*=item.itemValue;
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.speedMultiplier*=nextLevelItem.itemValue;
        }

    }

    public void IncreaseDamage(item item) //handles attack effect type items
    {
        if(item.initiallevel ==1)
        {
            player.playerAttack*=item.itemValue;
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.playerAttack*=nextLevelItem.itemValue;
        }


    }

    public void IncreaseAttackSpeed(item item) //handle attack speed effect type items
    {
        if(item.initiallevel ==1)
        {
            player.playerAtkSpd*=item.itemValue;
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.playerAtkSpd*=nextLevelItem.itemValue;
        }

    }

    public void IncreaseExpGain(item item) //handle experience effect type items
    {
        
        if(item.initiallevel ==1)
        {
            player.expGain*=item.itemValue;
        }
        else//stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.expGain*=nextLevelItem.itemValue;
        }

    }

    public void ReduceDamageTaken(item item) //handle defense effect type items
    {
        if(item.initiallevel ==1)
        {
            player.reduceDamage*=item.itemValue;
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.reduceDamage*=nextLevelItem.itemValue;
        }
    }
    
}
