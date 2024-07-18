using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the base class for enemies

public class EnemyController : MonoBehaviour
{
    // Enemy stats
    EnemyStats stats;

    // rigidbody of enemy
    protected Rigidbody2D rb;

    protected GameObject player;
    protected Transform playerPos;

    protected GameObject gameController;

    protected Vector2 moveDir;

    // current health of enemy
    protected int currentHealth;

    // Attack state
    private bool isAttacking = false;

    public void InitializeEnemy(EnemyStats initstats)
    {
        this.stats = initstats;
        this.currentHealth = stats.maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController");
        if (player != null)
        {
            playerPos = player.transform;
            //Debug.Log("Player found!");
        }
        else
        {
            Debug.Log("Player not found! Make sure the Player GameObject is tagged as 'Player'.");
        }
        moveDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // sprite rotation
        moveDir = (playerPos.position - transform.position).normalized;
        if(moveDir.magnitude!=0)
        {
            rb.transform.up = moveDir;
        }

        // if player in range and enemy is not already attacking
        if (IsPlayerInRange() && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    protected virtual void EnemyMovement()
    {
        rb.velocity = new Vector2(moveDir.x * stats.moveSpeed, moveDir.y * stats.moveSpeed);
    }

    protected virtual void EnemyAttack()
    {
        // if player exists
        if (player != null)
        {
            dummyScript playerScript = player.GetComponent<dummyScript>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(stats.damage);
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        while (IsPlayerInRange())
        {
            EnemyAttack();

            // Wait for the attack cooldown before attacking again
            yield return new WaitForSeconds(stats.attackCooldown);
        }

        isAttacking = false;
    }

    protected bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
        return distanceToPlayer <= stats.attackRange;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    protected virtual void EnemyDie()
    {
        EnemySpawner enemySpawnerScript = gameController.GetComponent<EnemySpawner>();
        enemySpawnerScript.DestroyEnemyPrefab(this.gameObject);
    }
    //- deletion of prefab
    //- ensure that prefab remove from object pooling properly
    //- ensure wave spawning works on second 3 etc wave
    //- ensure that spawning renames the objects
    //-for debugging to add a button to spawn next wave instantly, and add to clear all enemies at once
}
