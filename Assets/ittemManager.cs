using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the effect of items

public class ittemManager : MonoBehaviour
{
    private List<item> items = Game.GetItemList();

    private Player player;

    private PlayerInventory playerInventory;

    [SerializeField] UpgradeMenu upgradeMenu;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerInventory = FindObjectOfType<PlayerInventory>(); 
    }

    public void ApplyItemEffects(item item)
    {
        if(!upgradeMenu.replacingItem)
        {
            switch (item.effectType)
            {
                case "Health":
                    player.IncreaseMaxHealth(item);
                    break;
                case "Speed":
                    player.IncreaseMoveSpeed(item);
                    break;
                case "Attack":
                    player.IncreaseDamage(item);
                    break;
                case "Attack Speed":
                    player.IncreaseAttackSpeed(item);
                    break;
                case "Experience":
                    player.IncreaseExpGain(item);
                    break;
            }
        }
        else
        {
            // Debug.LogWarning("Replacing items, resetting stats!");

            player.health = Game.GetChar().health;

            player.speed = Game.GetChar().moveSpd;

            player.attackSpeed = Game.GetChar().atkSpd;

            player.attack = Game.GetChar().atkMultiplier;
            
            player.expMultiplier = 1f;
            player.pickupRange = 1f;
            player.incomingDamageMultiplier = 1f;

            // Debug.LogWarning("Stats reset!" +"Health:"+ player.health +" Speed:"+ player.speed + " Attack:" + player.attack + " Attack speed:" + player.attackSpeed  + " expMultiplier:" +   player.expMultiplier+ " pickup range:" + player.pickupRange + " incoming damage multiplier:" + player.incomingDamageMultiplier);

            foreach(item i in playerInventory.itemInventory)
            {
                switch (item.effectType)
                {
                    case "Health":
                        player.IncreaseMaxHealth(item);
                        break;
                    case "Speed":
                        player.IncreaseMoveSpeed(item);
                        break;
                    case "Attack":
                        player.IncreaseDamage(item);
                        break;
                    case "Attack Speed":
                        player.IncreaseAttackSpeed(item);
                        break;
                    case "Experience":
                        player.IncreaseExpGain(item);
                        break;
                    
                }
            }
            // Debug.LogWarning("Slot 1:" + playerInventory.itemInventory[0].name + playerInventory.itemInventory[0].initiallevel + playerInventory.itemInventory[0].itemValue);
            // Debug.LogWarning("Slot2:" + playerInventory.itemInventory[1].name + playerInventory.itemInventory[1].initiallevel + playerInventory.itemInventory[1].itemValue);
            // Debug.LogWarning("Slot3:" + playerInventory.itemInventory[2].name + playerInventory.itemInventory[2].initiallevel + playerInventory.itemInventory[2].itemValue);
            // Debug.LogWarning("Stats relenished!" +"Health:"+ player.health +" Speed:"+ player.speed + " Attack:" + player.attack + " Attack speed:" + player.attackSpeed  + " expMultiplier:" +   player.expMultiplier+ " pickup range:" + player.pickupRange + " incoming damage multiplier:" + player.incomingDamageMultiplier);
        }
        
    }
}
