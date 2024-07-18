using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<Character> charSelect = new List<Character>();
    public List<Weapon> weaponList = new List<Weapon>();

    public List<item> itemList = new List<item>();

    public List<WeaponUpgrades> weaponUpgradeList = new List<WeaponUpgrades>();
    public List<ItemUpgrades> itemUpgradesList = new List<ItemUpgrades>();

    public List<npcDialogue> npcDialoguesList = new List<npcDialogue>();
    public List<PlayerDialogue> playerDialoguesList = new List<PlayerDialogue>();
    
    
    public void LoadAllData()
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
    public void LoadCharacterData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Character.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refChar refchar = new refChar();
            refchar.id = columnData[0];
            refchar.charName = columnData[1];
            float.TryParse(columnData[2], out refchar.health);
            float.TryParse(columnData[3], out refchar.moveSpd);
            float.TryParse(columnData[4], out refchar.atkSpd);
            float.TryParse(columnData[5], out refchar.atkMultiplier);
            refchar.weaponID = columnData[6]; 

            Character chara = new Character(refchar.id, refchar.charName, refchar.health, refchar.moveSpd, refchar.atkSpd, refchar.atkMultiplier, refchar.weaponID);

            charSelect.Add(chara);

            //debug purposes
            // foreach(var c in charSelect)
            // {
            //     Debug.Log(c.charName);
            // }

            Game.SetCharList(charSelect);
        }
    }

    #endregion Load Character Data
    
    #region Weapon Data

    public void LoadWeaponData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Weapons.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refWeapon refWeapon = new refWeapon();
            refWeapon.id = columnData[0];
            refWeapon.name = columnData[1];
            refWeapon.weaponType = columnData[2];
            float.TryParse(columnData[3], out refWeapon.damage);
            refWeapon.basicDesc = columnData[4]; 
            refWeapon.isGeneric = columnData[5].ToLower() == "true";

            Weapon weapon = new Weapon(refWeapon.id, refWeapon.name, refWeapon.damage, refWeapon.weaponType, refWeapon.basicDesc, refWeapon.isGeneric);

            weaponList.Add(weapon);

            //debug purposes
            // foreach(var w in weaponList)
            // {
            //     Debug.Log(w.name);
            // }

            Game.SetWeaponList(weaponList);
        }
    }

    #endregion Weapon Data

    #region Item Data

    public void LoadItemData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Items.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refItem refitem = new refItem();
            refitem.id = columnData[0];
            refitem.name = columnData[1];
            refitem.effectType = columnData[2];
            refitem.basicDesc = columnData[3];

            item Item = new item(refitem.id, refitem.name, refitem.effectType, refitem.basicDesc);

            itemList.Add(Item);

            //debug purposes
            // foreach(var item in itemList)
            // {
            //     Debug.Log(item.name);
            // }

            Game.SetItemList(itemList);
        }
    }

    #endregion Item Data

    #region Weapon Upgrades

    public void LoadWeaponUpgrades()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Weapon Upgrades.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refWeaponUpgrades refUpgrades = new refWeaponUpgrades();
            refUpgrades.refID = columnData[0];
            int.TryParse(columnData[1], out refUpgrades.level);
            int.TryParse(columnData[2], out refUpgrades.projectileCount);
            float.TryParse(columnData[3], out refUpgrades.dmgMultiplier);
            float.TryParse(columnData[4], out refUpgrades.fireRate);
            refUpgrades.upgradeDesc = columnData[5];

            WeaponUpgrades weaponUpgrades = new WeaponUpgrades(refUpgrades.refID,refUpgrades.level,refUpgrades.projectileCount,refUpgrades.dmgMultiplier, refUpgrades.fireRate, refUpgrades.upgradeDesc);

            weaponUpgradeList.Add(weaponUpgrades);

            //debug purposes
            // foreach(var item in itemList)
            // {
            //     Debug.Log(item.name);
            // }

            Game.SetWeaponUpgradesList(weaponUpgradeList);
        }
    }

    #endregion Weapon Upgrades

    #region Item upgrades

    public void LoadItemUpgrades()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Item Upgrades.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refItemUpgrades refUpgrades = new refItemUpgrades();
            refUpgrades.itemID = columnData[0];
            int.TryParse(columnData[1], out refUpgrades.level);
            refUpgrades.upgradeDesc = columnData[2];

            ItemUpgrades itemUpgrades = new ItemUpgrades(refUpgrades.itemID, refUpgrades.level, refUpgrades.upgradeDesc);

            itemUpgradesList.Add(itemUpgrades);

            //debug purposes
            // foreach(var item in itemList)
            // {
            //     Debug.Log(item.name);
            // }

            Game.SetItemUpgradesList(itemUpgradesList);
        }
    }

    #endregion Item upgrades

    #region  NPC Dialogues

    public void LoadNpcDialogues()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/NPC Dialogue.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refNpcDialogue refDialogue = new refNpcDialogue();
            refDialogue.id = columnData[0];
            refDialogue.dialogue = columnData[1];
            refDialogue.dialogueBy = columnData[2];
            refDialogue.isDialogueSelection = columnData[3].ToLower() == "true";
            refDialogue.optionResponseID = columnData[4];

            npcDialogue dialogue = new npcDialogue(refDialogue.id, refDialogue.dialogue,  refDialogue.dialogueBy,  refDialogue.isDialogueSelection,  refDialogue.optionResponseID);

            npcDialoguesList.Add(dialogue);

            Game.SetNpcDialogueList(npcDialoguesList);
        }
    }

    #endregion NPC Dialogues

    #region Player Dialogues

    public void LoadPlayerDialogues()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/Player Dialogue.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refPlayerDialogue refDialogue = new refPlayerDialogue();
            refDialogue.triggerID = columnData[0];
            refDialogue.id = columnData[1];
            refDialogue.dialogue = columnData[2];
            refDialogue.dialogueBy = columnData[3];
            refDialogue.dialogueType = columnData[4];
            refDialogue.typeID = columnData[5];

            PlayerDialogue dialogue = new PlayerDialogue(refDialogue.triggerID, refDialogue.id, refDialogue.dialogue,  refDialogue.dialogueBy,  refDialogue.dialogueType,  refDialogue.typeID);

            playerDialoguesList.Add(dialogue);

            Game.SetPlayerDialogueList(playerDialoguesList);
        }
    }

    #endregion Player Dialogues
    
}
