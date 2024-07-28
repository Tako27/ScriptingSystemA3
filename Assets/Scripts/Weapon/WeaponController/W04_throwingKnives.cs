using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the sub class of weapon and overrides the behaviour of the base

public class W04_throwingKnives : WeaponController
{
    public float projectileSpeed = 10f;
    public float spreadAngle = 30f; // Angle between projectiles


    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        int projectileCount = weaponRef.projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = (i - (projectileCount - 1) / 2f) * spreadAngle;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * InputHandler.instance.GetMousePosition();

            GameObject weaponEffectObject = base.FireWeapon(firePosition);

            if (weaponEffectObject.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.velocity = direction * projectileSpeed;
            }
        }

        return null;
    }
}
