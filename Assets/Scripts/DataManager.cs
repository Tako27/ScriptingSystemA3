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
    
    
    public void LoadAllData()
    {
        LoadCharacterData();
        LoadItemData();
        LoadWeaponData();
    }

    #region Load Character Data
    public void LoadCharacterData()
    {
        string filePath = Path.Combine(Application.dataPath, "Data/A3 - Character - Static.csv");
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
        string filePath = Path.Combine(Application.dataPath, "Data/A3 - Weapons - Static.csv");
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
        string filePath = Path.Combine(Application.dataPath, "Data/A3 - Items - Static.csv");
        string[] fileData = File.ReadAllLines(filePath);
        for(int i =1; i<fileData.Length;i++)
        {
            string[] columnData = fileData[i].Split(new char[] { ',' });

            refItem refItem = new refItem();
            refItem.id = columnData[0];
            refItem.name = columnData[1];
            refItem.effectType = columnData[2];

            item Item = new item(refItem.id, refItem.name, refItem.effectType);

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
    
}
