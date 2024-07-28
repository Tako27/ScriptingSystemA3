using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the sub class of weapon and overrides the behaviour of the base
public class W07_SacrificialKnife : WeaponController
{
    protected override WeaponEffectData CalculateWeaponEffectData()
    {
        WeaponEffectData newEffectData = base.CalculateWeaponEffectData();

        newEffectData.weaponRadius = 1f;

        return newEffectData;
    }

    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        Vector3 mouseDirection = InputHandler.instance.GetMousePosition();

        return base.FireWeapon(firePosition + (mouseDirection * attackRange * 1f));
    }
}
