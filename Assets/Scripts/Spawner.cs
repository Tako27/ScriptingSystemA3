using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public string enemyID;
    public float spawnRate;
    public int spawnCount;
    public int prefabID;
}

public class Spawner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 300f;
    

    public int enemyCount;
    public int waveNo = 0;
    public string selectedMapID;

    public GameObject enemy;

    // enemy spawn information dictionary  [Waves] [waveNo, enemyID, spawnRate, spawnCount]
    public Dictionary<int, List<EnemySpawnInfo>> waveIDToEnemySpawnDict;

    public Dictionary<string, EnemyStats> enemyStatsInfoDict;

    public List<GameObject> enemyPrefabList;

    public List<Transform> spawnPointList;

    public List<CoroutineWrapper> enemySpawnerCoroutineList;

    bool waveIsDone = true;


    private void Start()
    {
        // read the enemy info [waves] csv of selected map and populate the Dictionary
        waveIDToEnemySpawnDict = new Dictionary<int, List<EnemySpawnInfo>>();
       
        // for loop all waves information [each wave i], add each enemy [waveNo, List of enemies]

            List<EnemySpawnInfo> enemySpawnInfos = new List<EnemySpawnInfo>(); // list of enemies
            enemySpawnInfos.Add(new EnemySpawnInfo()); //add each enemy inside the list

            //waveIDToEnemySpawnDict.Add(i, enemySpawnInfos);

        // read enemy stats csv [Enemy] and populate the enemyStatsInfoDict Dictionary

        // new EnemyStats and set the information
    }

    void Update()
    {
        Debug.Log(waveNo);

        if (waveIsDone)
        {
            waveNo++;
            // current wave

            List<EnemySpawnInfo> enemySpawnInfos = waveIDToEnemySpawnDict[waveNo];

            // for each enemy spawn info

                // spawn a coroutine
                CoroutineWrapper newCoroutine = new CoroutineWrapper(this, EnemySpawnerCoroutine(new EnemySpawnInfo()));
                newCoroutine.Start();
                enemySpawnerCoroutineList.Add(newCoroutine);
                // retrieve wave information

            waveIsDone = false;
        } 
        else
        {
            for (int i = 0; i < enemySpawnerCoroutineList.Count; i++)
            {
                if (enemySpawnerCoroutineList[i].IsRunning)
                {
                    return;
                }
            }

            waveIsDone = true;
            enemySpawnerCoroutineList.Clear();
        }
    }

    IEnumerator EnemySpawnerCoroutine(EnemySpawnInfo enemySpawnInfo)
    {
        float spawnTimerCounter = timeBetweenWaves;

        while (spawnTimerCounter > 0f)
        {
            spawnTimerCounter -= spawnTimerCounter;

            for (int i = 0; i < enemyCount; i++)
            {
                // spawn enemy prefab change transform.position /.rotation to spawnpoint [spawnPointList]
                GameObject enemyClone = Instantiate(enemyPrefabList[enemySpawnInfo.prefabID], transform.position, transform.rotation);

                // get the enemy script
                Enemy enemyCloneComponent = enemyClone.GetComponent<Enemy>();
            
                // set enemy stats once spawn
                enemyCloneComponent.InitializeEnemyStats(enemyStatsInfoDict[enemySpawnInfo.enemyID]);


            }

            yield return new WaitForSeconds(spawnRate);
        }

    }

    public void SpawnEnemy()
    {

    }
}
