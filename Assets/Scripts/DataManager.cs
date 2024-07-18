using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //Done by Lee Ying Jie
    public List<Character> charSelect = new List<Character>();
    public List<Weapon> weaponList = new List<Weapon>();

    public List<item> itemList = new List<item>();

    public List<WeaponUpgrades> weaponUpgradeList = new List<WeaponUpgrades>();
    public List<ItemUpgrades> itemUpgradesList = new List<ItemUpgrades>();

    public List<npcDialogue> npcDialoguesList = new List<npcDialogue>();
    public List<PlayerDialogue> playerDialoguesList = new List<PlayerDialogue>();
    
    
    public void LoadAllData() //method to load all data from csv files
    {
        LoadCharacterData();
        LoadItemData();
        LoadWeaponData();
        LoadWeaponUpgrades();
        LoadItemUpgrades();
        LoadNpcDialogues();
        LoadPlayerDialogues();
    }

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
            //set the variables of character class according to variables from refchar class
            Character chara = new Character(refchar.id, refchar.charName, refchar.health, refchar.moveSpd, refchar.atkSpd, refchar.atkMultiplier, refchar.weaponID);

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
            refWeapon refWeapon = new refWeapon();
            refWeapon.id = columnData[0];
            refWeapon.name = columnData[1];
            refWeapon.weaponType = columnData[2];
            float.TryParse(columnData[3], out refWeapon.damage); //convert from string to float
            refWeapon.basicDesc = columnData[4]; 
            refWeapon.isGeneric = columnData[5].ToLower() == "true"; //convert from string to bool

            Weapon weapon = new Weapon(refWeapon.id, refWeapon.name, refWeapon.damage, refWeapon.weaponType, refWeapon.basicDesc, refWeapon.isGeneric);
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

            item Item = new item(refitem.id, refitem.name, refitem.effectType, refitem.basicDesc);

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
            refUpgrades.upgradeDesc = columnData[5];

            WeaponUpgrades weaponUpgrades = new WeaponUpgrades(refUpgrades.refID,refUpgrades.level,refUpgrades.projectileCount,refUpgrades.dmgMultiplier, refUpgrades.fireRate, refUpgrades.upgradeDesc);

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

            ItemUpgrades itemUpgrades = new ItemUpgrades(refUpgrades.itemID, refUpgrades.level, refUpgrades.upgradeDesc);

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
