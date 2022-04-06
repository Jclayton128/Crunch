using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WeaponBehavior : MonoBehaviour
{
    protected Transform targetTransform;
    protected Vector3 targetPosition;
    protected float lifetime;
    protected float chargePower;

    protected Rigidbody2D rb;
    [SerializeField] float initialSpeed; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(Transform targetTransform, float lifetime, float chargePower)
    {
        this.targetTransform = targetTransform;
        this.lifetime = lifetime;
        this.chargePower = chargePower;

        InternalInitialize();
    }
    public void Initialize(Vector3 targetPosition, float lifetime, float chargePower)
    {
        this.targetPosition = targetPosition;
        this.lifetime = lifetime;
        this.chargePower = chargePower;

        InternalInitialize();
    }

    private void InternalInitialize()
    {
        rb.velocity = transform.up * initialSpeed;
        Destroy(gameObject, lifetime);
    }
}
