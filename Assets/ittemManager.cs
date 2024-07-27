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

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void ApplyItemEffects()
    {
        foreach (item item in playerInventory.itemInventory)
        {
            switch (item.effectType)
            {
                case "Health":
                    HealthEffect(item.itemValue);
                    break;
                case "Regen":
                    //StartCoroutine(Regeneration(item.itemValue, item.effectCooldown));
                    break;
                case "Speed":
                    MovementSpeed(item.itemValue);
                    break;
                case "Attack":
                    Attack(item.itemValue);
                    break;
                case "Attack Speed":
                    AttackSpeed(item.itemValue);
                    break;
                case "Defense":
                    Defense(item.itemValue);
                    break;
                case "Experience":
                    ExperienceGain(item.itemValue);
                    break;
                case "Collection":
                    Collection(item.itemValue);
                    break;
            }
        }
    }

    private void HealthEffect(float value)
    {
        player.IncreaseMaxHealth(value);
    }

    private IEnumerator Regeneration(float percentage, float cooldown)
    {
        while (true)
        {
            player.RegenerateHealth(percentage);
            yield return new WaitForSeconds(cooldown); // Assuming regeneration happens every 10 seconds
        }
    }

    private void MovementSpeed(float multiplier)
    {
        player.IncreaseMoveSpeed(multiplier);
    }

    private void Attack(float multiplier)
    {
        player.IncreaseDamage(multiplier);
    }

    private void AttackSpeed(float multiplier)
    {
        player.IncreaseAttackSpeed(multiplier);
    }

    private void Defense(float multiplier)
    {
        player.ReduceDamageTaken(multiplier);
    }

    private void ExperienceGain(float multiplier)
    {
        player.IncreaseExpGain(multiplier);
    }

    private void Collection(float multiplier)
    {
        player.IncreasePickUpRange(multiplier);
    }
}
