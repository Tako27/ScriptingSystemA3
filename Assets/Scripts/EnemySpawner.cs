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

    // list of spawn points, dont need to assign in inspector
    // ensure that in each map prefabs there is an empty gameobject called "SpawnPoints"
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
    // constant 5 minustes in seconds assuming each wave duration is always 5 minutes [5 * 60f]
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
            if (currentWaveNo > GetTotalWaveCount())
            {
                Debug.Log("All waves completed.");
                StopSpawning();
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
            if (!isSpawningActive)
            {
                // Exit the coroutine if spawning is no longer active
                yield break;
            }

            // Check if spawnRate is 0, indicating a boss spawn
            if (enemySpawnInfo.spawnRate == 0f)
            {
                // Spawn the boss once and then break out of the loop
                Transform spawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
                GameObject enemyClone = GetEnemyPrefab(enemySpawnInfo.enemyID, spawnPoint, ++noOfEnemySpawned);
                if (enemyClone == null)
                {
                    Debug.LogError("Failed to get enemy prefab for ID: " + enemySpawnInfo.enemyID);
                }
                else
                {
                    Debug.Log("Spawned boss: " + enemyClone.name);
                }
                yield break;
            }

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

    // to initialize all the enemy object pool at the start by the enemyID
    public void InitializeObjectPool(string enemyID, int initialSize)
    {
        GameObject prefab = SearchEnemyPrefabList(enemyID);
        if (prefab != null)
        {
            string key = prefab.name;
            // if there is no existing queue for the enemy type, create new queue
            if (!objectPool.ContainsKey(key))
            {
                objectPool[key] = new Queue<GameObject>();
            }

            // for the size of the object pool i want
            for (int i = 0; i < initialSize; i++)
            {
                // instantiate all the prefab
                GameObject obj = Instantiate(prefab);
                // set it to active false
                obj.SetActive(false);
                // add it to the queue
                objectPool[key].Enqueue(obj);
            }
        }
    }

    public void CreateObjectPool()
    {
        // to ensure that each enemy type only has 1 object pool even if it appears in different waves
        List<string> enemyIds = new List<string>();
        // check what enemies this map has and create object pool based on that
        foreach (EnemySpawnInfo info in enemySpawnInfoList)
        {
            // if enemy is not a boss
            if (info.spawnRate != 0)
            {
                // run through enemy prefab list
                foreach (GameObject enemyPrefab in enemyPrefabList)
                {
                    // if the prefab name matches the enemyID in the enemySpawnInfoList
                    // and if there is no existing object pool for that enemyID
                    if (enemyPrefab.name == info.enemyID && !enemyIds.Contains(enemyPrefab.name))
                    {
                        // Initial size of 15, adjust as needed
                        InitializeObjectPool(enemyPrefab.name, 15);
                        enemyIds.Add(info.enemyID);
                    }
                }

            }
            // if enemy is a boss
            else
            {
                foreach (GameObject enemyPrefab in enemyPrefabList)
                {
                    if (enemyPrefab.name == info.enemyID)
                    {
                        // Initial size of 15, adjust as needed
                        InitializeObjectPool(enemyPrefab.name, 1);
                    }
                }
            }
        }
    }

    // handles spawning from object pool
    public GameObject GetEnemyPrefab(string enemyID, Transform spawnPoint, int noOfEnemySpawned)
    {
        GameObject prefab = SearchEnemyPrefabList(enemyID);
        if (prefab != null)
        {
            string key = prefab.name;
            GameObject obj;

            // if object pool contains the prefab
            if (objectPool.ContainsKey(key) && objectPool[key].Count > 0)
            {
                obj = objectPool[key].Dequeue();
                //reset obj position and rotation
                obj.transform.position = spawnPoint.position;
                obj.transform.rotation = spawnPoint.rotation;
            }
            // obj does not exist in pool
            else
            {
                // create new obj from prefab
                obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            }

            // reactivating obj
            obj.SetActive(true);

            // rename enemy object before spawning
            obj.name = prefab.name + "_" + noOfEnemySpawned;

            // Reset the enemy's state
            EnemyController enemyCloneScript = obj.GetComponent<EnemyController>();
            if (enemyCloneScript != null)
            {
                enemyCloneScript.InitializeEnemy(Game.GetEnemyByID(enemyID));
            }

            // return the obj
            return obj;
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
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;

        // Reset any other necessary properties here
        EnemyController enemyCloneScript = obj.GetComponent<EnemyController>();
        if (enemyCloneScript != null)
        {
            enemyCloneScript.ResetEnemy();
        }

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
        // start creating object pool based on the enemy type in the map
        CreateObjectPool();

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

    public void GetSpawnPointsByMap(string selectedMapID)
    {
        // find the gameobject of the map in the scene
        GameObject mapPrefab = GameObject.Find(selectedMapID + "(Clone)");

        // find the "SpawnPoints" child
        Transform spawnPointsTransform = mapPrefab.transform.Find("SpawnPoints");

        Debug.Log("Found SpawnPoints: " + spawnPointsTransform.name);

        // Run through all the children in the mapSpawnPoints and add to the list
        for (int i = 0; i < spawnPointsTransform.childCount; i++)
        {
            Transform spawnPoint = spawnPointsTransform.GetChild(i);
            spawnPointList.Add(spawnPoint);
        }
    }

    // get total number of waves in chosen map
    public int GetTotalWaveCount()
    {
        int waveCount = enemySpawnInfoList[enemySpawnInfoList.Count - 1].waveNo;
        
        return waveCount;
    }

    public void StartSpawning()
    {
        // initialize the wave number and set the spawning flag
        currentWaveNo = 0;
        waveIsDone = true;
        isSpawningActive = true;
    }
    public void StopSpawning()
    {
        // set the spawning flag to false to stop new spawning
        isSpawningActive = false;

        // stop all running coroutines
        foreach (var coroutine in enemySpawnerCoroutineList)
        {
            if (coroutine.IsRunning)
            {
                coroutine.Stop();
            }
        }

        // clear the coroutine list
        enemySpawnerCoroutineList.Clear();

        // set waveIsDone to true to reset the wave state
        waveIsDone = true;
    }
}
