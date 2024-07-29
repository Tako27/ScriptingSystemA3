using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Code Done By: Celest Goh Zi Xuan
// ================================
// This script controls the subclass of weapon Controller
public class W05_Axe : WeaponController
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
