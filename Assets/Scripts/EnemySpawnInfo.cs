using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script holds the configuration for spawning enemies
// in waves within a specific map.

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
