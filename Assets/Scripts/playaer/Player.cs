using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the player stats, as well as application of weapons and items
public class Player : MonoBehaviour
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
    private ittemManager itemManager;

    private List<ItemUpgrades> itemUpgrades;

    private bool initializedStats;

    void Start()
    {
        initializedStats = false;
        playerMovement = GetComponent<playerMovement>();
        playerInventory = GetComponent<PlayerInventory>();
        itemManager = GetComponent<ittemManager>();

        itemUpgrades = Game.GetItemUpgradesList();

    }
    void Update()
    {
        
        if(gameController.gameActive)
        {
            
            while(!initializedStats)
            {
                health = Game.GetChar().health;
                speed = Game.GetChar().moveSpd;
                attackSpeed = Game.GetChar().atkSpd;
                attack = Game.GetChar().atkMultiplier;

                initializedStats = true;
            }
            
        }
        
    }

    public void IncreaseMaxHealth(item item)
    {
        if(item.initiallevel ==1)
        {
            health+=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". Health increased by:" + item.itemValue + ". Current health:" + health );
        }
        else
        {
            ItemUpgrades nextLevelItem = itemUpgrades.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            health+=nextLevelItem.itemValue;
            // Debug.Log("Item level:" + item.initiallevel + ". Health increased by:" + item.itemValue + ". Current health:" + + health );
        }
    }

    public void IncreaseMoveSpeed(item item)
    {
        if(item.initiallevel ==1)
        {
            speed = playerMovement.movementSpeed*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". Spped increased by:" + item.itemValue + ". Current Speed:" + speed );
        }
        else
        {
            ItemUpgrades nextLevelItem = itemUpgrades.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            speed = playerMovement.movementSpeed*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". Speed increased by:" + item.itemValue + ". Current speed:"  + speed );
        }

    }

    public void IncreaseDamage(item item)
    {
        if(item.initiallevel ==1)
        {
            attack*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack mulptiplier increased by:" + item.itemValue + ". Current multiplier:"  + attack );
        }
        else
        {
            ItemUpgrades nextLevelItem = itemUpgrades.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            attack*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack multiplier increased by:" + item.itemValue + ". Current multiplier:"  + attack );
        }


    }

    public void IncreaseAttackSpeed(item item)
    {
        if(item.initiallevel ==1)
        {
            attackSpeed*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack speed mulptiplier increased by:" + item.itemValue + ". Current multiplier:" + attackSpeed );
        }
        else
        {
            ItemUpgrades nextLevelItem = itemUpgrades.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            attackSpeed*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack speed multiplier increased by:" + item.itemValue + ". Current multiplier:" + attackSpeed );
        }

    }

    public void IncreaseExpGain(item item)
    {
        
        if(item.initiallevel ==1)
        {
            expMultiplier*=item.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". exp multiplier increased by:" + item.itemValue + ". Current multiplier:" + expMultiplier);
        }
        else
        {
            ItemUpgrades nextLevelItem = itemUpgrades.Find(upgrade => upgrade.level == item.initiallevel); //get the stats of current level for this item
            expMultiplier*=nextLevelItem.itemValue;
            // Debug.Log( "Item level:" + item.initiallevel + ". attack multiplier increased by:" + item.itemValue + ". Current multiplier:" + expMultiplier);
        }

    }
}
