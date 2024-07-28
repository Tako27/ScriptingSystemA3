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

    private static npcDialogue npcDialogue;

    private static PlayerDialogue playerDialogue;

    private static WeaponUpgrades weaponUpgrades;

    private static ItemUpgrades itemUpgrades;
    private static List<Character> charList;
    private static List<Weapon> weaponList;
    private static List<item> itemList;

    private static List<npcDialogue> npcDialogueList;

    private static List<PlayerDialogue> playerDialogueList;

    private static List<WeaponUpgrades> weaponUpgradeList;

    private static List<ItemUpgrades> itemUpgradesList;

    private static List<EnemyStats> enemyStatsList;
    private static List<EnemySpawnInfo> enemySpawnInfoList;

    private static List<string> mapIDs = new List<string>();
    private static string mapIDSelected;

    private static string time;

    private static string playerLevel;

    private static int totalEnemiesKilled;
    private static Dictionary<string, int> typeOfEnemiesKilled = new Dictionary<string, int>();

    public static Character GetChar()
    {
        return chara;
    }
    public static void SetChar(Character achar)
    {
        chara = achar;
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

    public static void SetItemList(List<item> aList)
    {
        itemList = aList;
    }

    public static List<item> GetItemList()
    {
        return itemList;
    }

    public static item GetItemByEffectType(string aEffectType)
    {
        return itemList.Find(x => x.effectType == aEffectType);
    }

    public static void SetWeaponUpgradesList(List<WeaponUpgrades> list)
    {
        weaponUpgradeList = list;
    }

    public static List<WeaponUpgrades> GetWeaponUpgradesList()
    {
        return weaponUpgradeList;
    }

    public static void SetItemUpgradesList(List<ItemUpgrades> list)
    {
        itemUpgradesList = list;
    }

    public static List<ItemUpgrades> GetItemUpgradesList()
    {
        return itemUpgradesList;
    }

    public static npcDialogue GetNpcDialogue()
    {
        return npcDialogue;
    }
    private static void SetNpcDialogue(npcDialogue aDialogue)
    {
        npcDialogue = aDialogue;
    }

    public static void SetNpcDialogueList(List<npcDialogue> aList)
    {
        npcDialogueList = aList;
    }

    public static List<npcDialogue> GetNpcDialogueList()
    {
        return npcDialogueList;
    }

    public static PlayerDialogue GetPlayerDialogue()
    {
        return playerDialogue;
    }
    private static void SetPlayerDialogue(PlayerDialogue aDialogue)
    {
        playerDialogue = aDialogue;
    }

    public static void SetPlayerDialogueList(List<PlayerDialogue> aList)
    {
        playerDialogueList = aList;
    }

    public static List<PlayerDialogue> GetPlayerDialogueList()
    {
        return playerDialogueList;
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

    public static void AddTotalEnemiesKilled()
    {
        totalEnemiesKilled++;
    }

    public static void ResetEnemiesKilledCounters()
    {
        totalEnemiesKilled = 0;
        typeOfEnemiesKilled.Clear();
    }

    public static int GetTotalEnemiesKilled() 
    {  
        return totalEnemiesKilled; 
    }

    public static void AddTypeOfEnemiesKilled(string enemyID)
    {
        string key = enemyID;
        // if key exist just add 1
        if (typeOfEnemiesKilled.ContainsKey(key))
        {
            typeOfEnemiesKilled[key]++;
        }
        // if key doesnt exist set to 1
        else
        {
            typeOfEnemiesKilled[key] = 1;
        }
    }

    public static Dictionary<string, int> GetTypeOfEnemiesKilled()
    {
        return typeOfEnemiesKilled;
    }

    public static void SetTime(string atime)
    {
        time = atime;
    }

    public static string GetTime()
    {
        return time;
    }

    public static void SetLevel(string alevel)
    {
        playerLevel = alevel;
    }

    public static string GetLevel()
    {
        return playerLevel;
    }
}
