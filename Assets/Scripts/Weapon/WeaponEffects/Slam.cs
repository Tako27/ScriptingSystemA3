using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
