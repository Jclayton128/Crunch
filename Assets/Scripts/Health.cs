using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    [SerializeField] protected float healthMax;
    [SerializeField] protected float healthCurrent;

    protected virtual void Start()
    {
        healthCurrent = healthMax;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dd;
        if (collision.TryGetComponent<DamageDealer>(out dd))
        {
            ReceiveDamage(dd.Damage);
            dd.ImpactTarget();
        }
    }

    public virtual void ReceiveDamage(float incomingDamage)
    {
        healthCurrent -= incomingDamage;
        CheckForDeath();
    }

    protected void CheckForDeath()
    {
        if (healthCurrent <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
