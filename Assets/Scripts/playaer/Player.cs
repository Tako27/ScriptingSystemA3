using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the player stats, as well as application of items
public class Player : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
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

    public void TakeDamage(int damage) //handle taking damage
    {
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
        ittemManager.health = Game.GetChar().health;

        ittemManager.speed = Game.GetChar().moveSpd;

        ittemManager.attackSpeed = Game.GetChar().atkSpd;

        ittemManager.attack = Game.GetChar().atkMultiplier;
            
        ittemManager.expMultiplier = 1f;
        ittemManager.pickupRange = 1f;
        ittemManager.incomingDamageMultiplier = 1f;

        Debug.LogWarning("Stats reset!" +"Health:"+ ittemManager.health +" Speed:"+ ittemManager.speed + " Attack:" + ittemManager.attack + " Attack speed:" + ittemManager.attackSpeed  + " expMultiplier:" +   ittemManager.expMultiplier+ " pickup range:" + ittemManager.pickupRange + " incoming damage multiplier:" + ittemManager.incomingDamageMultiplier);

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
            // Debug.LogWarning("Slot 1:" + playerInventory.itemInventory[0].name + playerInventory.itemInventory[0].initiallevel + playerInventory.itemInventory[0].itemValue);
            // Debug.LogWarning("Slot2:" + playerInventory.itemInventory[1].name + playerInventory.itemInventory[1].initiallevel + playerInventory.itemInventory[1].itemValue);
            // Debug.LogWarning("Slot3:" + playerInventory.itemInventory[2].name + playerInventory.itemInventory[2].initiallevel + playerInventory.itemInventory[2].itemValue);
            Debug.LogWarning("Stats relenished!" +"Health:"+ ittemManager.health +" Speed:"+ ittemManager.speed + " Attack:" + ittemManager.attack + " Attack speed:" + ittemManager.attackSpeed  + " expMultiplier:" +   ittemManager.expMultiplier+ " pickup range:" + ittemManager.pickupRange + " incoming damage multiplier:" + ittemManager.incomingDamageMultiplier);
        
    }
}
