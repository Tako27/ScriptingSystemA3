using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyScript : MonoBehaviour
{
    // Maximum health of the player
    public int maxHealth = 100;
    // Current health of the player
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Decrease current health by the damage amount
        //Debug.Log("Player took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // Call Die method if health drops to 0 or below
        }
    }

    // Method to handle player death
    void Die()
    {
        // Add logic for what happens when the player dies (e.g., trigger animation, restart level, etc.)
        Debug.Log("Player died.");
        // For example, you might want to destroy the player object or trigger a respawn
        // Destroy(gameObject);
    }
}
