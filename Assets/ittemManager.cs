using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the effect of items

public class ittemManager : MonoBehaviour
{

     public float health;

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
    private ittemManager itemManager;

    private List<ItemUpgrades> itemUpgrades;

    private bool initializedStats;

    void Start()
    {
        initializedStats = false;
        playerMovement = GetComponent<playerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        itemManager = GetComponent<ittemManager>();
        player = GetComponent<Player>();    

        itemUpgrades = Game.GetItemUpgradesList();

    }
    void Update()
    {
        
        if(gameController.gameActive) //initialize stats the moment dialogue scene closes
        {
            
            while(!initializedStats)
            {
                health = Game.GetChar().health;
                speed = Game.GetChar().moveSpd;
                attackSpeed = Game.GetChar().atkSpd;
                attack = Game.GetChar().atkMultiplier;

                player.maxHealth = health;
                player.currentHealth = player.maxHealth;
                

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
            // Debug.Log( "Item level:" + item.initiallevel + ". Health increased by:" + item.itemValue + ". Current health:" + health );
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            player.maxHealth+=nextLevelItem.itemValue;
            player.currentHealth += nextLevelItem.itemValue;
            // Debug.Log("Item level:" + item.initiallevel + ". Health increased by:" + item.itemValue + ". Current health:" + + health );
        }
    }

    public void IncreaseMoveSpeed(item item) //handles speed effect type items
    {
        if(item.initiallevel ==1)
        {
            speed*=item.itemValue;
            Debug.Log( "Item level:" + item.initiallevel + ". Spped increased by:" + item.itemValue + ". Current Speed:" + speed );
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            speed*=nextLevelItem.itemValue;
            Debug.Log( "Item level:" + item.initiallevel + ". Speed increased by:" + item.itemValue + ". Current speed:"  + speed );
        }

    }

    public void IncreaseDamage(item item) //handles attack effect type items
    {
        if(item.initiallevel ==1)
        {
            attack*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack mulptiplier increased by:" + item.itemValue + ". Current multiplier:"  + attack );
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            attack*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack multiplier increased by:" + item.itemValue + ". Current multiplier:"  + attack );
        }


    }

    public void IncreaseAttackSpeed(item item) //handle attack speed effect type items
    {
        if(item.initiallevel ==1)
        {
            attackSpeed*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack speed mulptiplier increased by:" + item.itemValue + ". Current multiplier:" + attackSpeed );
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            attackSpeed*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack speed multiplier increased by:" + item.itemValue + ". Current multiplier:" + attackSpeed );
        }

    }

    public void IncreaseExpGain(item item) //handle experience effect type items
    {
        
        if(item.initiallevel ==1)
        {
            expMultiplier*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". exp multiplier increased by:" + item.itemValue + ". Current multiplier:" + expMultiplier);
        }
        else//stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            expMultiplier*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack multiplier increased by:" + item.itemValue + ". Current multiplier:" + expMultiplier);
        }

    }

    public void ReduceDamageTaken(item item) //handle defense effect type items
    {
        if(item.initiallevel ==1)
        {
            incomingDamageMultiplier*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". incoming dmg multiplier increased by:" + item.itemValue + ". Current multiplier:" + incomingDamageMultiplier);
        }
        else //stats of items that are not level 1
        {
            List<ItemUpgrades> upgradeList = Game.GetItemUpgradesList().FindAll(upgrade => upgrade.itemID == item.id);
            ItemUpgrades nextLevelItem = upgradeList.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            incomingDamageMultiplier*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". incoming dmg multiplier increased by:" + item.itemValue + ". Current multiplier:" + incomingDamageMultiplier);
        }
    }
    
}
