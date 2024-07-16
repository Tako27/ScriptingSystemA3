using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Enemy stats
    EnemyStats stats;

    // remember to change all this to stats.[stats]
    float attackRange = 1f;
    int damage = 1;
    float moveSpeed = 3f;
    float attackCooldown = 2f;

    // rigidbody of enemy
    protected Rigidbody2D rb;

    protected GameObject player;
    protected Transform playerPos;

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
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    protected virtual void EnemyAttack()
    {
        // if player exists
        if (player != null)
        {
            dummyScript playerScript = player.GetComponent<dummyScript>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damage);
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
            yield return new WaitForSeconds(attackCooldown);
        }

        isAttacking = false;
    }

    protected bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
        return distanceToPlayer <= attackRange;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
