using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Code Done By: Lee Ying Jie
// ================================
// This script handles the UI for the health bar
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI health;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.maxValue = maxHealth; //set max slider value to maximum health for the character classs\
        slider.value = currentHealth; //set current health --> reflect on exp bar
        UpdateHealthBarInfo(currentHealth, maxHealth);
    }

    public void UpdateHealthBarInfo(float currentHealth, float maxHealth) //set health text
    {
        health.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
