using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrunchInput), typeof(Rigidbody2D))]
public class CrunchMovement : MonoBehaviour
{
    CrunchInput input;
    Rigidbody2D rb;

    //settings
    [SerializeField] float _moveForce;

    void Start()
    {
        input = GetComponent<CrunchInput>();
        rb = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(input.MoveSignal_Horizontal) > float.Epsilon)
        {
            rb.AddForce(_moveForce * input.MoveSignal_Horizontal * Vector2.right);
        }
    }
}

