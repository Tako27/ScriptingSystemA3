using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script handles the player attack

public class PlayerAttack : PlayerScript, IAttackReceiver
{
    private GameController gameController;

    // Attack variables
    private bool isAttacking = false;
    public float attackCooldown = 1f; // Time between attacks

    // Reference to the player's weapon or attack point
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    public override void Initialize(GameController gameController)
    {
        this.gameController = gameController;
    }
    public void AttackAction(Vector2 attackDirection)
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack(attackDirection));
        }
    }

    private IEnumerator Attack(Vector2 attackDirection)
    {
        isAttacking = true;

        // perform attack (e.g., trigger animation, deal damage)
        //Debug.Log("Player attacks in direction: " + attackDirection);

        // Detect enemies in range of the attack
        RaycastHit2D[] hitEnemies = Physics2D.CircleCastAll(attackPoint.position, attackRange, attackDirection, attackRange, enemyLayers);

        // Damage the enemies
        foreach (RaycastHit2D enemy in hitEnemies)
        {
            //Debug.Log("Hit " + enemy.collider.name);

            // !!!### REMEBER TO CHANGE TO VALUE AFTER ADDING WEAPON MULTIPLIER ###!!!
            enemy.collider.GetComponent<EnemyController>().TakeDamage(80);
        }

        // Wait for the attack cooldown before allowing another attack
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
