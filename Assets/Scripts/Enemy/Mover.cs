using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Navigator), typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] protected float _moveForce = 0;
    protected Navigator _navigator;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _navigator = GetComponent<Navigator>();
        _rb = GetComponent<Rigidbody2D>();
    }
}
