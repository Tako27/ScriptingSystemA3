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
    public float attackRadius = 0.05f;
    public LayerMask enemyLayers;

    private PlayerInventory playerInventory;

    public GameObject staffProjectilePrefab;

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

        //foreach (Weapon weapon in playerInventory.GetWeaponInventory())
        //{
        //    //weapon.Attack(attackPoint, attackDirection);
        //}

        StaffAttack(attackPoint, attackDirection);
        // Detect enemies in range of the attack
        //RaycastHit2D[] hitEnemies = Physics2D.CircleCastAll(attackPoint.position, attackRadius, attackDirection, attackRange, enemyLayers);
        //
        //// Damage the enemies
        //foreach (RaycastHit2D enemy in hitEnemies)
        //{
        //    //Debug.Log("Hit " + enemy.collider.name);
        //
        //    // !!!### REMEBER TO CHANGE TO VALUE AFTER ADDING WEAPON MULTIPLIER ###!!!
        //    enemy.collider.GetComponent<EnemyController>().TakeDamage(80);
        //}

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

    public int GetProjectileCountPerWeapon(string weaponID)
    {
        List<Weapon> weaponList = Game.GetWeaponList();
        foreach(Weapon weapon in weaponList)
        {
            if (weapon.id == weaponID)
            {
                return weapon.projectileCount;
            }
        }
        // if the weaponId do not match any weapon in the list
        return -1;
    }

    public void StaffAttack(Transform attackPoint, Vector2 attackDirection)
    {
        float projectileSpeed = 10f;

        float spreadAngle = 30f; // Angle between projectiles

        int projectileCount = GetProjectileCountPerWeapon("W01");

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = (i - (projectileCount - 1) / 2f) * spreadAngle;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * attackDirection;

            GameObject projectile = Instantiate(staffProjectilePrefab, attackPoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }
    }
}
