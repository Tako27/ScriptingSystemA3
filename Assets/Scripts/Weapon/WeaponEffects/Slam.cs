using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the sub class of weapon effects and overrides the behaviour of the base
public class Slam : WeaponEffect
{
    public override void InitializeWeaponEffect(WeaponEffectData weaponEffectData)
    {
        base.InitializeWeaponEffect(weaponEffectData);

        transform.localScale *= weaponEffectData.weaponRadius;
    }

    protected override void HandleWeaponEffectTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.TryGetComponent<EnemyController>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
