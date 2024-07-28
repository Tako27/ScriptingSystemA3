using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Code Done By: Lee Ying Jie
// ================================
// This script is handles the display of inventory ui during gameplay
public class GameUI : MonoBehaviour
{
    [SerializeField] List<Image> weaponInvetoryImage;
    [SerializeField] List<Image> itemInventoryImage;

    [SerializeField] TextMeshProUGUI enemiesKilledText;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }
    
    void Update() //set inventory ui
    {
        for(int i =0; i<playerInventory.weaponInventory.Count;i++)
        {
            string spriteFilePath = playerInventory.weaponInventory[i].imageFilePath;
            Sprite weaponImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
            weaponInvetoryImage[i].sprite = weaponImage;
        }

        for(int i =0; i<playerInventory.itemInventory.Count;i++)
        {
            string spriteFilePath = playerInventory.itemInventory[i].imageFilePath;
            Sprite itemImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
            itemInventoryImage[i].sprite = itemImage;
        }

        enemiesKilledText.text = "Total Enemies killed: " + Game.GetTotalEnemiesKilled();
    }
}
