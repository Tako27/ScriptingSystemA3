using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string selectedMapID;

    public float spawnRate;
    public float timeBetweenWaves = 300f;

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

            // current wave 
            List<EnemySpawnInfo> enemySpawnInfos = waveIDToEnemySpawnDict[currentWaveNo];

            foreach (var esi in enemySpawnInfos)
            {
                CoroutineWrapper newCoroutine = new CoroutineWrapper(this, EnemySpawnerCoroutine(new EnemySpawnInfo()));
            }
        }
    }

    IEnumerator EnemySpawnerCoroutine(EnemySpawnInfo enemySpawnInfo)
    {
        float elapsedTime = 0f;

        // true as long as elapsed time is lesser than 5 minutes
        while (elapsedTime < waveDuration)
        {
            for (int i = 0; i < enemySpawnInfo.spawnCount; i++)
            {
                // spawning enemy prefab at random spawn point
                Transform spawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
                //spawning gameobject from objectPool
                GameObject enemyClone = GetEnemyPrefab(enemySpawnInfo.enemyID, spawnPoint);

                //get the enemy script
                //set enemy stats after spawned
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

    public GameObject GetEnemyPrefab(string enemyID, Transform spawnPoint)
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
                // return the obj
                return obj;
            }
            // obj does not exist in pool
            else
            {
                // create new obj from prefab
                GameObject obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                obj.name = prefab.name;
                return obj;
            }
        }
        Debug.Log("enemyID not found");
        return null;
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
