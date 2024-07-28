using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W08_ShockwaveHammer : WeaponController
{
    protected override WeaponEffectData CalculateWeaponEffectData()
    {
        WeaponEffectData newEffectData = base.CalculateWeaponEffectData();
        Debug.Log(weaponRef.weaponRangeMultiplier);
        newEffectData.weaponRadius = weaponRef.weaponRangeMultiplier;

        return newEffectData;
    }

    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        Vector3 mouseDirection = InputHandler.instance.GetMousePosition();

        return base.FireWeapon(firePosition + (mouseDirection * attackRange * weaponRef.weaponRangeMultiplier));
    }
    // need check if the range multiplier works properly
    
}
