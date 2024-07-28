using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W01_Staff : WeaponController
{
    [Header("Staff Stats")]
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
