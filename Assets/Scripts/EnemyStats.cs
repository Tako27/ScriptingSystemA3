using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
