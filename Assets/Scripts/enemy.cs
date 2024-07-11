using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy stats
    public string enemyID { get; private set; }
    public string enemyName { get; private set; }
    public int maxHealth { get; private set; }
    public float moveSpeed { get; private set; }
    public int damage { get; private set; }
    public int enemyPrefabNo { get; private set; }

    // current health of enemy
    protected int currentHealth;

    // rigidbody of enemy
    protected Rigidbody2D rb;

    protected Transform player;

    protected Vector2 moveDir;

    // Initialize enemy stats
    public void Initialize(string enemyID, string enemyName, int maxHealth, float moveSpeed, int damage, int enemyPrefabNo)
    {
        this.enemyID = enemyID;
        this.enemyName = enemyName;
        this.maxHealth = maxHealth;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.enemyPrefabNo = enemyPrefabNo;
        this.currentHealth = maxHealth;
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
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    protected virtual void EnemyAttack()
    {
        // to be overidden by enemy behaviour scripts
    }

}
