using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the player stats, as well as application of weapons and items
public class Player : MonoBehaviour
{
    public float health;

    public float speed;
    public float attack;
    public float expMultiplier = 1f;

    public float attackSpeed;
    public float pickupRange = 1f;
    public float incomingDamageMultiplier = 1f;

    private playerMovement playerMovement;
    private PlayerInventory playerInventory;

    [SerializeField] GameController gameController;
    [SerializeField] ittemManager itemManager;

    void Start()
    {
        
        playerMovement = GetComponent<playerMovement>();

        if(gameController.gameActive)
        {
            health = Game.GetChar().health;
            speed = Game.GetChar().moveSpd;
            attackSpeed = Game.GetChar().atkSpd;
            attack = Game.GetChar().atkMultiplier;
        }
        

    }
    void Update()
    {
        if(gameController.gameActive)
        {
        
            foreach(item item in playerInventory.itemInventory)
            {
                itemManager.ApplyItemEffects();
            }
        }
        
    }

    public void IncreaseMaxHealth(float amount)
    {
        health+=amount;
        Game.GetChar().health = health;
        Debug.Log("Health increased by:" + amount + ". Current health:" + health);
    }

    public void RegenerateHealth(float percentge)
    {
        float amount = health*percentge;
        Debug.Log("Regeneration not coded in yet");
    }

    public void IncreaseMoveSpeed(float amount)
    {
        playerMovement.movementSpeed *= amount;
        Game.GetChar().moveSpd = speed;
        Debug.Log("Movespeed increased by:" + amount + ". Current movespeed:" + playerMovement.movementSpeed);
    }

    public void IncreaseDamage(float amount)
    {
        attack*=amount;
        Game.GetChar().atkMultiplier = attack;
        Debug.Log("Damage increased by:" + amount + ". Current damage:" + attack);
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackSpeed *= amount;
        Game.GetChar().atkSpd = attackSpeed;
        Debug.Log("Attack spped increased by:" + amount + ". Current attack speed:" + attackSpeed);
    }

    public void ReduceDamageTaken(float amount)
    {
        Debug.Log("not coded in yet");
    }

    public void IncreaseExpGain(float amount)
    {
        expMultiplier*=amount;
        Debug.Log("Exp gain increased by:" + amount + ". Current exp gain multiplier:" + expMultiplier);
    }

    public void IncreasePickUpRange(float amount)
    {
        pickupRange*=amount;
        Debug.Log("Pickup range increased by:" + amount + ". Current pickup range:" + pickupRange);
    }
}
