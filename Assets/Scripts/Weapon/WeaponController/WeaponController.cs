using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is the base class for weapon
public abstract class WeaponController : MonoBehaviour
{
    // weapon data from player inventory
    public Weapon weaponRef;

    [Header("Base Weapon Stats")]
    // Weapon Attack Range and Radius
    public float attackRange = 1f;
    public float attackRadius = 0.05f;

    // Reference to the player's weapon or attack point
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public GameObject weaponEffectPrefab;

    protected bool weaponEnabled = false;

    [SerializeField]
    private float attackTimer = 0f;

    public void InitializeWeapon(Weapon weaponRef)
    {
        this.weaponRef = weaponRef;

        this.weaponEnabled = true;
        attackTimer = 0f;
}

    public void DisableWeapon()
    {
        this.weaponEnabled = false;
    }

    // firing weapon
    protected virtual GameObject FireWeapon(Vector3 firePosition)
    {
        GameObject weaponEffectObject = Instantiate(weaponEffectPrefab, firePosition, Quaternion.identity);
        // try get component so dont have to write if null
        if (weaponEffectObject.TryGetComponent<WeaponEffect>(out var weaponEffect))
        {
            weaponEffect.InitializeWeaponEffect(CalculateWeaponEffectData());
        }

        return weaponEffectObject;
    }

    private void Update()
    {
        if (weaponEnabled)
        {
            HandleWeaponTick();
        }
    }

    // auto firing
    private void HandleWeaponTick()
    {
        if (attackTimer <= 0f)
        {
            FireWeapon(attackPoint.position);
            attackTimer = 1 / (weaponRef.fireRate * Game.GetChar().atkSpd);
            return;
        }

        attackTimer -= Time.deltaTime;
    }

    // calculate damage before passing to weapon effect
    protected virtual WeaponEffectData CalculateWeaponEffectData()
    {
        WeaponEffectData newEffectData = new WeaponEffectData();
        newEffectData.damage = weaponRef.damage * weaponRef.dmgMultiplier * Game.GetChar().atkMultiplier;

        return newEffectData;
    }

}
