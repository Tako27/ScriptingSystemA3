using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnemyStats
{
    public string enemyID;
    public string enemyName;
    public int maxHealth;
    public int damage;
    public float moveSpeed;
} 

public abstract class Enemy : MonoBehaviour
{
    protected EnemyStats enemyStats;

    protected int currentHealth;

    protected Rigidbody2D rb;

    protected Transform player;

    protected Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        moveDir = Vector2.zero;
    }

    public void InitializeEnemyStats(EnemyStats initStats)
    {
        this.enemyStats = initStats;
        this.currentHealth = enemyStats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Sprite Rotation
        moveDir = (player.position - transform.position).normalized;
        if (moveDir.magnitude != 0)
        {
            rb.transform.up = moveDir;
        }

        //EnemyAttack();
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    protected virtual void EnemyMovement()
    {
        rb.velocity = new Vector2(moveDir.x * enemyStats.moveSpeed, moveDir.y * enemyStats.moveSpeed);
    }

    protected virtual void EnemyAttack() { }
}
