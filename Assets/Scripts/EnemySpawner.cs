using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script controls the enemy spawning

public class EnemySpawner : MonoBehaviour
{
    public string selectedMapID;

    public float spawnRate;

    public List<GameObject> enemyPrefabList;
    public List<Transform> spawnPointList;

    // to hold all enemy spawn info from game script based on the selected map
    public List<EnemySpawnInfo> enemySpawnInfoList;

    // to hold the enemy spawn infos per wave
    public Dictionary<int, List<EnemySpawnInfo>> waveIDToEnemySpawnDict;

    // dictionary for object pooling, [key] separates the types of enemy prefab
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    private int currentWaveNo = 0;
    private bool waveIsDone = true;
    // Control flag for spawning
    private bool isSpawningActive = false;
    // Coroutine wrapper
    private List<CoroutineWrapper> enemySpawnerCoroutineList = new List<CoroutineWrapper>();
    // constant 5 minustes in seconds assuming each wave duration is always 5 minutes
    private const float waveDuration = 5 * 60f;

    void Update()
    {
        // Do nothing if spawning is not active
        if (!isSpawningActive) return;

        // debugging purposes
        //Debug.Log(currentWaveNo);

        if (waveIsDone)
        {
            currentWaveNo++;

            Debug.Log(currentWaveNo);
            // if all waves finish spawning exit spawn loop
            if (currentWaveNo >= GetTotalWaveCount())
            {
                Debug.Log("All waves completed.");
                isSpawningActive = false;
                return;
            }

            // current wave 
            List<EnemySpawnInfo> enemySpawnInfos = waveIDToEnemySpawnDict[currentWaveNo];

            foreach (var esi in enemySpawnInfos)
            {
                CoroutineWrapper newCoroutine = new CoroutineWrapper(this, EnemySpawnerCoroutine(esi));
                newCoroutine.Start();
                enemySpawnerCoroutineList.Add(newCoroutine);
            }
            // set wave is done is false, so it doesnt spawn the next wave before the wave spawning is completed
            waveIsDone = false;
        }
        // if wave is not done
        else
        {
            // run through to the coroutine list to check if the coroutine is still running
            foreach (var coroutine in enemySpawnerCoroutineList)
            {
                // if coroutine still running, break out of loop
                if (coroutine.IsRunning)
                {
                    return;
                }
            }
            // if not running then set wave is done to true, and clear out the coroutine list
            waveIsDone = true;
            enemySpawnerCoroutineList.Clear();
        }
    }

    IEnumerator EnemySpawnerCoroutine(EnemySpawnInfo enemySpawnInfo)
    {
        float elapsedTime = 0f;
        // tracks how many of a type of enemy spawned
        int noOfEnemySpawned = 0;

        // true as long as elapsed time is lesser than 5 minutes
        while (elapsedTime < waveDuration)
        {
            for (int i = 0; i < enemySpawnInfo.spawnCount; i++)
            {
                noOfEnemySpawned++;
                // spawning enemy prefab at random spawn point
                Transform spawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
                //spawning gameobject from objectPool
                GameObject enemyClone = GetEnemyPrefab(enemySpawnInfo.enemyID, spawnPoint, noOfEnemySpawned);
                if (enemyClone == null)
                {
                    Debug.LogError("Failed to get enemy prefab for ID: " + enemySpawnInfo.enemyID);
                    continue;
                }

                // get the enemy script
                EnemyController enemyCloneScript = enemyClone.GetComponent<EnemyController>();

                //set enemy stats after spawned
                if (enemyCloneScript != null)
                {
                    enemyCloneScript.InitializeEnemy(Game.GetEnemyByID(enemySpawnInfo.enemyID));
                }
                else
                {
                    Debug.LogError("Enemy script not found on instantiated prefab.");
                }
            }
            // wait for seconds till the next group spawn
            yield return new WaitForSeconds(enemySpawnInfo.spawnRate);
            // accumulate the elapsed time
            elapsedTime += enemySpawnInfo.spawnRate;
        }
    }
    // for searching which prefab to use from prefab list depending on the enemyID in enemySpawnInfo
    public GameObject SearchEnemyPrefabList(string enemyID)
    {
        foreach(GameObject obj in enemyPrefabList)
        {
            if (obj.name == enemyID)
            {
                return obj;
            }
        }
        Debug.Log("enemyID not found");
        return null;
    }

    // handles spawning from object pool
    public GameObject GetEnemyPrefab(string enemyID, Transform spawnPoint, int noOfEnemySpawned)
    {
        GameObject prefab = SearchEnemyPrefabList(enemyID);
        if (prefab != null)
        {
            string key = prefab.name;
            // if object pool contains the prefab
            if (objectPool.ContainsKey(key) && objectPool[key].Count > 0)
            {
                GameObject obj = objectPool[key].Dequeue();

                // reactivating obj
                obj.SetActive(true);

                // rename enemy object before spawning
                obj.name = prefab.name + "_" + noOfEnemySpawned;

                // return the obj
                return obj;
            }
            // obj does not exist in pool
            else
            {
                // create new obj from prefab
                GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                obj.name = prefab.name + "_" + noOfEnemySpawned;
                return obj;
            }
        }
        Debug.LogError("Enemy prefab not found in pool or prefab list for ID: " + enemyID);
        return null;
    }

    public void DestroyEnemyPrefab(GameObject obj)
    {
        string key = obj.name.Split('_')[0];
        if (!objectPool.ContainsKey(key)) 
        {
            objectPool[key] = new Queue<GameObject>(); 
        }
        obj.SetActive(false);
        objectPool[key].Enqueue(obj);
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
