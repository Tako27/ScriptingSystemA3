using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Code Done By: Lee Ying Jie, Celest Goh Zi Xuan
// ================================
// This script stores the game data pulled from the csv
// and any static variables

public static class Game
{
    private static Character chara;
    private static Weapon weapon;
    private static item item;
    private static List<Character> charList;
    private static List<Weapon> weaponList;
    private static List<item> itemList;
    
    private static List<EnemyStats> enemyStatsList;
    private static List<EnemySpawnInfo> enemySpawnInfoList;
    
    private static List<string> mapIDs = new List<string>();
    private static string mapIDSelected;

    public static Character GetChar()
    {
        return chara;
    }
    public static void SetChar(Character achar)
    {
        chara = achar;
    }

    public static Character GetCharByID(string id)
    {
        return charList.Find(x => x.id == id);;
    }
    
    public static void SetCharList(List<Character> aList)
    {
        charList = aList;
    }
    public static List<Character> GetCharList()
    {
        return charList;
    }

    public static Weapon GetWeapon()
    {
        return weapon;
    }
    private static void SetWeapon(Weapon aweapon)
    {
        weapon = aweapon;
    }

    public static Weapon GetWeaponByID(string id)
    {
        return weaponList.Find(x=>x.id == id);
    }

    public static void SetWeaponList(List<Weapon> aList)
    {
        weaponList = aList;
    }

    public static List<Weapon> GetWeaponList()
    {
        return weaponList;
    }

    public static item GetItem()
    {
        return item;
    }
    private static void SetItem(item aitem)
    {
        item = aitem;
    }

    public static item GetItemByID(string id)
    {
        return itemList.Find(x=>x.id == id);
    }

    public static void SetItemList(List<item> aList)
    {
        itemList = aList;
    }

    public static List<item> GetItemList()
    {
        return itemList;
    }

    // this is to show player all the map available to choose from
    public static List<string> GetMapIDs()
    {
        foreach (var esi in enemySpawnInfoList)
        {
            // if list is empty add first
            if (mapIDs.Count == 0)
            {
                mapIDs.Add(esi.mapID);
            }
            // if list does not contain the same map id then add
            else if (!mapIDs.Contains(esi.mapID))
            {
                mapIDs.Add(esi.mapID);
            }
            // if list contain the same map id continue
            else
            {
                continue;
            }
        }

        // debug purposes
        //foreach (var m in mapIDs)
        //{
        //    Debug.Log(m);
        //}

        return mapIDs;
    }

    // this is to get the enemy stats based on the enemyID
    public static EnemyStats GetEnemyByID(string id)
    {
        return enemyStatsList.Find(x => x.enemyID == id);
    }

    // this is save the full enemy stats info
    public static void SetEnemyStatsList(List<EnemyStats> aList)
    {
        enemyStatsList = aList;
    }

    // this is get the full enemy stats info
    public static List<EnemyStats> GetEnemyStatsList()
    {
        return enemyStatsList;
    }

    // this is get enemy spawn info based on which map the player select
    public static List<EnemySpawnInfo> GetEnemySpawnInfoByMap(string id)
    {
        List<EnemySpawnInfo> spawnInfoByMap = new List<EnemySpawnInfo>();
        foreach (var esi in enemySpawnInfoList)
        {
            if (esi.mapID == id)
            {
                spawnInfoByMap.Add(esi);
            }
        }
        return spawnInfoByMap;
    }

    // this is to save the full csv of enemy spawn info
    public static void SetEnemySpawnInfoList(List<EnemySpawnInfo> aList)
    {
        enemySpawnInfoList = aList;
    }

    // this is to get the full csv of the enemy spawn info
    public static List<EnemySpawnInfo> GetEnemySpawnInfoList()
    {
        return enemySpawnInfoList;
    }

    // this is to get which map the player selected
    public static string GetmapID()
    {
        // debug purposes
        //Debug.Log(mapIDSelected);

        return mapIDSelected;
    }

    // this is to set which map the player selected
    public static void SetMapID(string id)
    {
        mapIDSelected = id;
    }
}
