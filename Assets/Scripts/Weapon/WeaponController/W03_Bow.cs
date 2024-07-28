using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the sub class of weapon and overrides the behaviour of the base
public class W03_Bow : WeaponController
{
    public float projectileSpeed = 10f;
    public float spreadAngle = 30f; // Angle between projectiles

    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        int projectileCount = weaponRef.projectileCount;
        //Debug.Log(projectileCount);
        if (projectileCount > 1 )
        {
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
        else
        {
            Vector2 direction = InputHandler.instance.GetMousePosition();
            GameObject weaponEffectObject = base.FireWeapon(firePosition);
            if (weaponEffectObject.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.velocity = direction * projectileSpeed;
            }
            return null;
        }
        
    }
}
