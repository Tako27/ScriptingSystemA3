using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the player stats, as well as application of items
public class Player : MonoBehaviour
{
    public float maxHealth;

    public float speedMultiplier;
    public float currentHealth;
    public float playerAtkSpd;
    public float playerAttack;
    public float expGain;
    public float reduceDamage;
   private List<item> items = Game.GetItemList();

    private ittemManager ittemManager;

    private PlayerInventory playerInventory;

    [SerializeField] UpgradeMenu upgradeMenu;
    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        ittemManager = FindObjectOfType<ittemManager>();
        playerInventory = FindObjectOfType<PlayerInventory>(); 


    }

    void Update()
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage) //handle taking damage
    {
        damage*=reduceDamage;
        currentHealth -= damage;
        
        if(currentHealth<=0)
        {
            Die();
        }
        
    }

    public void Die() //handle death
    {

    }

    public void ApplyItemEffects() //apply effects of items to player
    {
        //reset all changes before applying new item effects to the player
        maxHealth = ittemManager.maxHealth;
        currentHealth-=ittemManager.healthAdded;

        speedMultiplier = ittemManager.speed;

        playerAtkSpd = ittemManager.attackSpeed;
        playerAttack = ittemManager.attack;
        expGain = ittemManager.expMultiplier;
        reduceDamage = ittemManager.incomingDamageMultiplier;

        ittemManager.healthAdded = 0;

        //apply new item effects
        foreach(item i in playerInventory.itemInventory)
        {
            switch (i.effectType)
            {
                case "Health":
                    ittemManager.IncreaseMaxHealth(i);
                    break;
                case "Speed":
                    ittemManager.IncreaseMoveSpeed(i);
                    break;
                case "Attack":
                    ittemManager.IncreaseDamage(i);
                    break;
                case "Attack Speed":
                    ittemManager.IncreaseAttackSpeed(i);
                    break;
                case "Experience":
                    ittemManager.IncreaseExpGain(i);
                    break;
                case "Defense":
                    ittemManager.ReduceDamageTaken(i);
                    break;
                }
            }
        
            Debug.LogWarning("Stats relenished!" +"Health:"+ maxHealth +" Speed:"+ ittemManager.speed + " Attack:" + ittemManager.attack + " Attack speed:" + ittemManager.attackSpeed  + " expMultiplier:" +   ittemManager.expMultiplier+ " pickup range:" + ittemManager.pickupRange + " incoming damage multiplier:" + ittemManager.incomingDamageMultiplier);
        
    }
}
