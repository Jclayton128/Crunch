using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponBehavior : MonoBehaviour
{
    WeaponController _wc;

    protected Rigidbody2D _rb;
    protected SpriteRenderer _sr;

    //public parameters
    public float Lifetime;
    public float Damage;
    public float Speed;
    //turn speed
    //snaking?
    public Sprite WeaponSprite;


    //state
    float _timeToDie;
    public Transform TargetTransform;
    public Transform TargetPosition;


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Execute Various Behaviours
        //

        CheckForLifetimeDeath();
    }

    public void Initialize(WeaponController weapConRef)
    {
        _wc = weapConRef;
    }

    public void SetAsNewWeapon()
    {
        _sr.sprite = WeaponSprite;
        _rb.velocity = transform.up * Speed;
        _timeToDie = Time.time + Lifetime;
    }

    private void CheckForLifetimeDeath()
    {
        if (Time.time > _timeToDie)
        {
            Debug.Log("Dying");
            _wc.ReturnWeapon(this);
            //Destroy(gameObject);
        }
    }
}
