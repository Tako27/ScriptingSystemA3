using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script is for pulling data from all the CSVs

public class DataManager : MonoBehaviour
{
    public List<Character> charSelect = new List<Character>();
    public List<Weapon> weaponList = new List<Weapon>();

    public List<item> itemList = new List<item>();

    public List<WeaponUpgrades> weaponUpgradeList = new List<WeaponUpgrades>();
    public List<ItemUpgrades> itemUpgradesList = new List<ItemUpgrades>();

    public List<npcDialogue> npcDialoguesList = new List<npcDialogue>();
    public List<PlayerDialogue> playerDialoguesList = new List<PlayerDialogue>();

    // creating list of enemystats & spawninfo
    public List<EnemyStats> enemyStatsList = new List<EnemyStats>();
    public List<EnemySpawnInfo> enemySpawnInfoList = new List<EnemySpawnInfo>();

    public void LoadAllData() //method to load all data from csv files
    {
        LoadCharacterData();
        LoadItemData();
        LoadWeaponData();
        LoadWeaponUpgrades();
        LoadItemUpgrades();
        LoadNpcDialogues();
        LoadPlayerDialogues();
        LoadEnemyStatsData();
        LoadWaveData();
    }

    #region Load Wave Data
    public void LoadWaveData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/A3 - Waves - Static.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for (int i = 1; i < fileData.Length; i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            // Create new enemy spawn info object and populate with data from the csv
            EnemySpawnInfo enemySpawnInfo = new EnemySpawnInfo
            {
                mapID = columnData[0],
                waveName = columnData[1],
                waveNo = int.Parse(columnData[2]),
                enemyID = columnData[3],
                spawnRate = float.Parse(columnData[4]),
                spawnCount = int.Parse(columnData[5])
            };

            // add the populated EnemyStats object to a list
            enemySpawnInfoList.Add(enemySpawnInfo);
        }

        // setting enemy list
        Game.SetEnemySpawnInfoList(enemySpawnInfoList);

        // debug purposes
        //foreach (var esi in enemySpawnInfoList)
        //{
        //    Debug.Log(esi.waveNo + esi.enemyID);
        //}
    }
    #endregion Load Wave Data

    #region Load Enemy Stats Data
    public void LoadEnemyStatsData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/A3 - Enemy - Static.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for (int i = 1; i < fileData.Length; i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            // Create new EnemyStats object and populate with data from the csv
            EnemyStats enemyStats = new EnemyStats
            {
                enemyID = columnData[0],
                enemyName = columnData[1],
                maxHealth = int.Parse(columnData[2]),
                moveSpeed = float.Parse(columnData[3]),
                damage = int.Parse(columnData[4]),
                enemyPrefabNo = int.Parse(columnData[5]),
                attackRange = float.Parse(columnData[6]),
                attackCooldown = float.Parse(columnData[7])
            };

            // add the populated EnemyStats object to a list
            enemyStatsList.Add(enemyStats);
        }

        // setting enemy list
        Game.SetEnemyStatsList(enemyStatsList);

        // debug purposes
        //foreach (var e in enemyStatsList)
        //{
        //    Debug.Log(e.enemyID + e.enemyName + e.maxHealth);
        //}
    }
    #endregion Load Enemy Stats Data

    #region Load Character Data
    public void LoadCharacterData() //load character data
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Character.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        //currently, fileData consists of data from each row, but is not separated by column
        for(int i =1; i<fileData.Length;i++) 
        {
            string[] columnData = fileData[i].Split(new char[] { ',' }); //this separates the data by column, it takes the data in fileData and splits it at each ","

            //now i need to assign each data based on their respective columns
            refChar refchar = new refChar(); 
            refchar.id = columnData[0];
            refchar.charName = columnData[1];
            float.TryParse(columnData[2], out refchar.health); //convert from string to float
            float.TryParse(columnData[3], out refchar.moveSpd);
            float.TryParse(columnData[4], out refchar.atkSpd);
            float.TryParse(columnData[5], out refchar.atkMultiplier);
            refchar.weaponID = columnData[6]; 
            refchar.spriteImage = columnData[7];
            
            //set the variables of character class according to variables from refchar class
            Character chara = new Character(refchar.id, refchar.charName, refchar.health, refchar.moveSpd, refchar.atkSpd, refchar.atkMultiplier, refchar.weaponID, refchar.spriteImage);

            charSelect.Add(chara); //add all initialized character ddata into this list
            //this list is acts as the main list of characters, which contains all available characters in the game
            //which will be referenced by the game script to set the character list in the game
            Game.SetCharList(charSelect); //this sets the character list
        }
    }

    #endregion Load Character Data
    
    #region Weapon Data

    public void LoadWeaponData() //load weapon data
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Weapons.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++) 
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refWeapon refweapon = new refWeapon();
            refweapon.id = columnData[0];
            refweapon.name = columnData[1];
            refweapon.weaponType = columnData[2];
            float.TryParse(columnData[3], out refweapon.damage); //convert from string to float
            refweapon.basicDesc = columnData[4]; 
            refweapon.isGeneric = columnData[5].ToLower() == "true"; //convert from string to bool
            refweapon.imageFilePath = columnData[6];

            Weapon weapon = new Weapon(refweapon.id, refweapon.name, refweapon.damage, refweapon.weaponType, refweapon.basicDesc, refweapon.isGeneric, refweapon.imageFilePath);
            weaponList.Add(weapon);
            //add all initialized weapon ddata into this list
            //this list is acts as the main list of weapon, which contains all available weapons in the game
            //which will be referenced by the game script to set the weapon list in the game
            Game.SetWeaponList(weaponList); //this sets the weapon list
        }
    }

    #endregion Weapon Data

    #region Item Data

    public void LoadItemData() //load item daata
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Items.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refItem refitem = new refItem();
            refitem.id = columnData[0];
            refitem.name = columnData[1];
            refitem.effectType = columnData[2];
            refitem.basicDesc = columnData[3];
            float.TryParse(columnData[4], out refitem.itemValue);
            refitem.effectTime = columnData[5];
            refitem.effectCooldown = columnData[6];
            refitem.imageFilePath = columnData[7];

            item Item = new item(refitem.id, refitem.name, refitem.effectType, refitem.basicDesc, refitem.itemValue, refitem.effectTime, refitem.effectCooldown, refitem.imageFilePath);

            itemList.Add(Item);
            //add all initialized item ddata into this list
            //this list is acts as the main list of item, which contains all available items in the game
            //which will be referenced by the game script to set the item list in the game

            Game.SetItemList(itemList); //set the item list
        }
    }

    #endregion Item Data

    #region Weapon Upgrades

    public void LoadWeaponUpgrades() //load weapon upgrades
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Weapon Upgrades.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refWeaponUpgrades refUpgrades = new refWeaponUpgrades();
            refUpgrades.refID = columnData[0];
            int.TryParse(columnData[1], out refUpgrades.level);
            int.TryParse(columnData[2], out refUpgrades.projectileCount);
            float.TryParse(columnData[3], out refUpgrades.dmgMultiplier);
            float.TryParse(columnData[4], out refUpgrades.fireRate);
            float.TryParse(columnData[5], out refUpgrades.weaponRangeMultiplier);
            float.TryParse(columnData[6], out refUpgrades.RegenProbability);
            float.TryParse(columnData[7], out refUpgrades.RecoveryMultiplier);
            refUpgrades.upgradeDesc = columnData[8];


            WeaponUpgrades weaponUpgrades = new WeaponUpgrades(refUpgrades.refID,refUpgrades.level,refUpgrades.projectileCount,refUpgrades.dmgMultiplier, refUpgrades.fireRate, refUpgrades.upgradeDesc, refUpgrades.weaponRangeMultiplier, refUpgrades.RegenProbability, refUpgrades.RecoveryMultiplier);

            weaponUpgradeList.Add(weaponUpgrades);
            //add all initialized weapon upgrade ddata into this list
            //this list is acts as the main list of weapon upgrade, which contains all available weapon upgrades in the game
            //which will be referenced by the game script to set the upgrades list in the game

            Game.SetWeaponUpgradesList(weaponUpgradeList); //set weapon upgrades list
        }
    }

    #endregion Weapon Upgrades

    #region Item upgrades

    public void LoadItemUpgrades() //load item upgrades 
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Item Upgrades.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refItemUpgrades refUpgrades = new refItemUpgrades();
            refUpgrades.itemID = columnData[0];
            int.TryParse(columnData[1], out refUpgrades.level);
            refUpgrades.upgradeDesc = columnData[2];
            float.TryParse(columnData[3], out refUpgrades.itemValue);
            refUpgrades.effectTime = columnData[4];
            refUpgrades.effectCooldown = columnData[5];

            ItemUpgrades itemUpgrades = new ItemUpgrades(refUpgrades.itemID, refUpgrades.level, refUpgrades.upgradeDesc, refUpgrades.itemValue, refUpgrades.effectTime, refUpgrades.effectCooldown);

            itemUpgradesList.Add(itemUpgrades);
            //add all initialized item upgrade ddata into this list
            //this list is acts as the main list of item upgrade, which contains all available item upgrades in the game
            //which will be referenced by the game script to set the upgrades list in the game

            Game.SetItemUpgradesList(itemUpgradesList); //set item upgrades list
        }
    }

    #endregion Item upgrades

    #region  NPC Dialogues

    public void LoadNpcDialogues() //load npc dialogue
    {
        string filePath = Path.Combine(Application.dataPath, "Data/NPC Dialogue.csv"); //find file from unity project menu
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refNpcDialogue refDialogue = new refNpcDialogue();
            refDialogue.id = columnData[0];
            refDialogue.dialogue = columnData[1];
            refDialogue.dialogueBy = columnData[2];
            refDialogue.isDialogueSelection = columnData[3].ToLower() == "true";
            refDialogue.optionResponseID = columnData[4];

            npcDialogue dialogue = new npcDialogue(refDialogue.id, refDialogue.dialogue,  refDialogue.dialogueBy,  refDialogue.isDialogueSelection,  refDialogue.optionResponseID);

            npcDialoguesList.Add(dialogue);
            //add all initialized npc dialogue ddata into this list
            //this list is acts as the main list of npc dialgoue, which contains all available npc dialogue in the game
            //which will be referenced by the game script to set the npc dialogue list in the game

            Game.SetNpcDialogueList(npcDialoguesList); //set npc dialogues
        }
    }

    #endregion NPC Dialogues

    #region Player Dialogues

    public void LoadPlayerDialogues() //load player dialogue
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Player Dialogue.csv"); //find file 
        string[] fileData = File.ReadAllLines(filePath); //pull data from file
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });
            //assign data based on their respective columns
            refPlayerDialogue refDialogue = new refPlayerDialogue();
            refDialogue.triggerID = columnData[0];
            refDialogue.id = columnData[1];
            refDialogue.dialogue = columnData[2];
            refDialogue.dialogueBy = columnData[3];
            refDialogue.dialogueType = columnData[4];
            refDialogue.typeID = columnData[5];

            PlayerDialogue dialogue = new PlayerDialogue(refDialogue.triggerID, refDialogue.id, refDialogue.dialogue,  refDialogue.dialogueBy,  refDialogue.dialogueType,  refDialogue.typeID);

            playerDialoguesList.Add(dialogue);
            //add all initialized player dialogue data into this list
            //this list acts as the main list of player dialogues, which contains all available plaer dialogue in game
            //which will be referenced by the game script to set the player dialogue list in game


            Game.SetPlayerDialogueList(playerDialoguesList); //set player dialogues
        }
    }

    #endregion Player Dialogues
    
}
