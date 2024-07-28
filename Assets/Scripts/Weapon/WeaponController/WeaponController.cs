using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
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
    }

    protected virtual GameObject FireWeapon(Vector3 firePosition)
    {
        GameObject weaponEffectObject = Instantiate(weaponEffectPrefab, firePosition, Quaternion.identity);

        if (weaponEffectObject.TryGetComponent<WeaponEffect>(out var weaponEffect))
        {
            weaponEffect.InitializeWeaponEffect(CalculateDamage());
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

    private void HandleWeaponTick()
    {
        if (attackTimer <= 0f)
        {
            FireWeapon(attackPoint.position);
            attackTimer = 1 / weaponRef.fireRate;
            return;
        }

        attackTimer -= Time.deltaTime;
    }

    protected float CalculateDamage()
    {
        return weaponRef.damage * weaponRef.dmgMultiplier * Game.GetChar().atkMultiplier;
    }

}
