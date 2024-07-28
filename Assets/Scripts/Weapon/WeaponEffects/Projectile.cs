using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : WeaponEffect
{
    protected override void HandleWeaponEffectTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<EnemyController>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the projectile upon hitting an enemy
        }
        else if (collision.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
