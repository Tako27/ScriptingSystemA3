using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the sub class of weapon and overrides the behaviour of the base
public class W08_ShockwaveHammer : WeaponController
{
    protected override WeaponEffectData CalculateWeaponEffectData()
    {
        WeaponEffectData newEffectData = base.CalculateWeaponEffectData();
        
        newEffectData.weaponRadius = weaponRef.weaponRangeMultiplier;

        return newEffectData;
    }

    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        Vector3 mouseDirection = InputHandler.instance.GetMousePosition();

        // fire position is player position, mouse direction is multiplied with the range (how far away from player)
        // weapon range multiplier multiplies range when upgraded
        return base.FireWeapon(firePosition + (mouseDirection * attackRange * weaponRef.weaponRangeMultiplier));
    }
    
}
