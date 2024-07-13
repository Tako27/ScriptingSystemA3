using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string selectedMapID;

    public float spawnRate;

    public List<GameObject> enemyPrefabList;
    public List<Transform> spawnPointList;

    public List<EnemySpawnInfo> enemySpawnInfoList;
    public Dictionary<int, List<EnemySpawnInfo>> waveIDToEnemySpawnDict;

    private int currentWaveNo = 0;
    private bool waveIsDone = true;
    // Coroutine wrapper
    private List<CoroutineWrapper> enemySpawnerCoroutineList = new List<CoroutineWrapper>();
    // Control flag for spawning
    private bool isSpawningActive = false;

    void Update()
    {
        // Do nothing if spawning is not active
        if (!isSpawningActive) return;

        // debugging purposes
        Debug.Log(currentWaveNo);

        if (waveIsDone)
        {
            currentWaveNo++;

            // if all waves finish spawning exit spawn loop
            if (currentWaveNo >= GetTotalWaveCount())
            {
                Debug.Log("All waves completed.");
                isSpawningActive = false;
                return;
            }


        }
    }

    public void GetWavesByMap()
    {
        // get chosen map from game script
        selectedMapID = Game.GetmapID();

        // get all enemy spawn info from game script based on the selected map
        enemySpawnInfoList = Game.GetEnemySpawnInfoByMap(selectedMapID);

        // storing spawn info by their wave number [int], [list of enemy spawn info in that wave number]
        waveIDToEnemySpawnDict = new Dictionary<int, List<EnemySpawnInfo>>();

        //starting with wave 1
        int waveNo = 1;
        // loop through all wave info and add the wave number, and list of enemy into the dictionary 
        for (int i = 0; i < GetTotalWaveCount(); i++)
        {
            List<EnemySpawnInfo> esiListPerWave = new List<EnemySpawnInfo>();
            foreach (var esi in enemySpawnInfoList)
            {
                if (esi.waveNo == waveNo)
                {
                    esiListPerWave.Add(esi);
                }
            }
            waveIDToEnemySpawnDict.Add(waveNo, esiListPerWave);
            waveNo++;

        }
        // debug purposes ===

        //foreach (var esi in enemySpawnInfoList)
        //{
        //    Debug.Log(esi.mapID + esi.waveNo + esi.enemyID);
        //}

        //Debug.Log(GetEnemyTypeCountPerWave(1));

        //foreach (KeyValuePair<int, List<EnemySpawnInfo>> kvp in waveIDToEnemySpawnDict)
        //{
        //    Debug.Log(kvp.Key);
        //    foreach (var e in kvp.Value)
        //    {
        //        Debug.Log(e.waveNo + " " + e.enemyID);
        //    }
        //}
    }

    // get total number of waves in chosen map
    public int GetTotalWaveCount()
    {
        int waveCount = enemySpawnInfoList[enemySpawnInfoList.Count - 1].waveNo;
        
        return waveCount;
    }

    public void StartSpawning()
    {
        // Initialize the wave number and set the spawning flag
        currentWaveNo = 0;
        waveIsDone = true;
        isSpawningActive = true;
    }

}
