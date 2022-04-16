using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : Health
{
    //settings
    int chanceToBeDamaged = 1;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer dd;
        if (collision.TryGetComponent<DamageDealer>(out dd))
        {
            int rand = UnityEngine.Random.Range(0, 11);
            if (rand <= chanceToBeDamaged)
            {
                ReceiveDamage(dd.Damage);
                dd.ImpactTarget();
            }
        }
    }


}
