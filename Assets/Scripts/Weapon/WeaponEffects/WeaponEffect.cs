using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponEffectData
{
    public float damage;
    public float weaponRadius;
    public float weaponRangeMultipler;
}

public abstract class WeaponEffect : MonoBehaviour
{
    public float damage;
    public float lifetime = 0.5f;

    private void OnEnable()
    {
        // Start the countdown to destroy the projectile
        StartCoroutine(DestroyAfterLifetime());
    }

    private IEnumerator DestroyAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }


    public virtual void InitializeWeaponEffect(WeaponEffectData weaponEffectData)
    {
        this.damage = weaponEffectData.damage;
    }

    protected abstract void HandleWeaponEffectTrigger(Collider2D collision);

    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleWeaponEffectTrigger(collision);
    }
}
