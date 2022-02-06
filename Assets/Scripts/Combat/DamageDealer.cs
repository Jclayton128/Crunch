using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] GameObject impactParticleFX = null;

    //state
    public float Damage { get; private set; } = 0;

    public virtual void SetDamage(float damage)
    {
        this.Damage = damage;
    }

    public virtual void ImpactTarget()
    {
        if (impactParticleFX)
        {
            Instantiate(impactParticleFX);
        }
        Destroy(gameObject);
    }

}
