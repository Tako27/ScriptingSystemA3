using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnInfo
{
    public string mapID;
    public string waveName;
    public int waveNo;
    public string enemyID;
    public float spawnRate;
    public int spawnCount;

    public string GetEnemySpawnInfoMapID()
    {
        return mapID;
    }
}
