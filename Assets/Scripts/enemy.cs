using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    EnemyStats stats;
    /*
    public string enemyID { get;}
    public string enemyName { get;}
    public int maxHealth { get;}
    public float moveSpeed { get;}
    public int damage { get;}
    public int enemyPrefabNo { get;}
    */
    public Enemy(EnemyStats stats)
    {
        this.
    }
    // current health of enemy
    protected int currentHealth;

    // rigidbody of enemy
    protected Rigidbody2D rb;

    protected Transform player;

    protected Vector2 moveDir;

    // Initialize enemy stats
    //public void InitializeEnemy(EnemyStats stats)
    //{
    //    enemyID = stats.enemyID;
    //    enemyName = stats.enemyName;
    //    maxHealth = stats.maxHealth;
    //    moveSpeed = stats.moveSpeed;
    //    damage = stats.damage;
    //    enemyPrefabNo = stats.enemyPrefabNo;
    //    currentHealth = maxHealth;
    //}

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
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    protected virtual void EnemyAttack()
    {
        // to be overidden by enemy behaviour scripts
    }

}
