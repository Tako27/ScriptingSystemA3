using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Code Done By: Lee Ying Jie
// ================================
// This script is for handling UI for exp bar
public class expBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI levelText;

    private Level level;

    void Start()
    {
        level = FindAnyObjectByType<Level>();
    }

    public void UpdateExpBar(int currentExp, int limit)
    {
        slider.maxValue = limit; //set max slider value to maximum exp for the current player level
        slider.value = currentExp; //set current level progress --> reflect on exp bar

        levelText.text = "Level: " + level.level.ToString(); //set text for player level
    }
}
