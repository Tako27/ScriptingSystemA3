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

    [SerializeField] List<TextMeshProUGUI> weaponInvetoryLevelText;
    [SerializeField] List<Image> itemInventoryImage;
    [SerializeField] List<TextMeshProUGUI> itemInventoryLevelText;

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
            #if UNITY_EDITOR
            Sprite weaponImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
            #endif
            weaponInvetoryImage[i].sprite = weaponImage; //set weapon image
            weaponInvetoryLevelText[i].text = "Lv: " + playerInventory.weaponInventory[i].initialLevel.ToString(); //set weapon level text
        }

        for(int i =0; i<playerInventory.itemInventory.Count;i++)
        {
            string spriteFilePath = playerInventory.itemInventory[i].imageFilePath;
            #if UNITY_EDITOR
            Sprite itemImage = AssetDatabase.LoadAssetAtPath<Sprite>(spriteFilePath);
            #endif
            itemInventoryImage[i].sprite = itemImage; //set item image
            itemInventoryLevelText[i].text = "Lv: " + playerInventory.itemInventory[i].initiallevel.ToString(); //set item level text
        }

        enemiesKilledText.text = "Total Enemies killed: " + Game.GetTotalEnemiesKilled(); //set number of enemies killed text
    }
}
