using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script holds the basic attributes and
// configurations for an enemy character in the game.

public class EnemyStats
{
    public string enemyID;
    public string enemyName;
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public int enemyPrefabNo;
    public float attackRange;
    public float attackCooldown;

    public string GetEnemyStatsID()
    {
        return enemyID;
    }
}
