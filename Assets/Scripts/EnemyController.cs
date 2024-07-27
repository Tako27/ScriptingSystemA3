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
    protected float currentHealth;

    // Attack state
    private bool isAttacking = false;

    public void InitializeEnemy(EnemyStats initstats)
    {
        this.stats = initstats;
        this.currentHealth = (float)stats.maxHealth;
    }

    public void ResetEnemy()
    {
        // Reset the state of the enemy
        isAttacking = false;
        currentHealth = stats.maxHealth;
        rb.velocity = Vector2.zero;
        // Add any other necessary resets
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
        if (currentHealth <= 0) 
        {
            EnemyDie();
        }

        // Check if GameObject is active before proceeding
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

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
            Player playerscript = player.GetComponent<Player>();
            if (playerscript != null)
            {
                playerscript.TakeDamage(stats.damage);
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
        if (stats != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        }
    }

    protected virtual void EnemyDie()
    {
        EnemySpawner enemySpawnerScript = gameController.GetComponent<EnemySpawner>();
        enemySpawnerScript.DestroyEnemyPrefab(this.gameObject);
        SpawnExp();
        Game.AddTotalEnemiesKilled();
        // Debug.Log(Game.GetTotalEnemiesKilled());
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }


    protected virtual void SpawnExp()
    {
        GameObject expDrop = gameController.GetComponent<GameController>().expDrop;
        Instantiate(expDrop, transform.position, transform.rotation);

    }
}
