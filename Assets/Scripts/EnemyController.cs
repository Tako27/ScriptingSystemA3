using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy stats
    EnemyStats stats;

    // rigidbody of enemy
    protected Rigidbody2D rb;

    protected Transform player;

    protected Vector2 moveDir;

    // current health of enemy
    protected int currentHealth;

    public void InitializeEnemy(EnemyStats initstats)
    {
        this.stats = initstats;
        this.currentHealth = stats.maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        moveDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // sprite rotation
        moveDir = (player.position - transform.position).normalized;
        if(moveDir.magnitude!=0)
        {
            rb.transform.up = moveDir;
        }

        // EnemyAttack();

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
        // to be overidden by enemy behaviour scripts
    }

}
