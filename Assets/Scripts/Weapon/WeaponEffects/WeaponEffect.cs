using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEffect : MonoBehaviour
{
    public float damage = 20f;
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


    public void InitializeWeaponEffect(float damage)
    {
        this.damage = damage;
    }

    protected abstract void HandleWeaponEffectTrigger(Collider2D collision);

    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleWeaponEffectTrigger(collision);
    }
}
