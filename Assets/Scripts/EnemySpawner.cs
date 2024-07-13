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
        }
    }

    public void GetWavesByMap()
    {
        selectedMapID = Game.GetmapID();
        // store what to spawn depending on which map player choose into list
        enemySpawnInfoList = Game.GetEnemySpawnInfoByMap(selectedMapID);

        // debug purposes
        //foreach (var esi in enemySpawnInfoList)
        //{
        //    Debug.Log(esi.mapID + esi.waveNo + esi.enemyID);
        //}
        //Debug.Log(GetEnemyTypeCountPerWave(1));
    }

    public int GetEnemyTypeCountPerWave(int waveNo)
    {
        int enemyTypeCount = 0;
        foreach (var esi in enemySpawnInfoList)
        {
            if (esi.waveNo == waveNo)
            {
                enemyTypeCount++;
            }
        }
        return enemyTypeCount;
    }

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
