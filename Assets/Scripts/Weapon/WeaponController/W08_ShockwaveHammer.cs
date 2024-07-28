using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W08_ShockwaveHammer : WeaponController
{
    protected override WeaponEffectData CalculateWeaponEffectData()
    {
        WeaponEffectData newEffectData = base.CalculateWeaponEffectData();
        newEffectData.weaponRadius = 1.25f; // * weaponRef.weaponRangeMultipler;

        return newEffectData;
    }

    protected override GameObject FireWeapon(Vector3 firePosition)
    {
        Vector3 mouseDirection = InputHandler.instance.GetMousePosition();

        return base.FireWeapon(firePosition + (mouseDirection * attackRange * 1.25f)); // * weaponRef.weaponRangeMultipler
    }

    
}
