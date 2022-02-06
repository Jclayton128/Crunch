using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JammerInput), typeof(Rigidbody2D))]

public class JammerMovement : MonoBehaviour
{
    JammerInput input;
    Rigidbody2D rb;
    SpriteRenderer sr;

    //settings
    float moveForce = 10f;
    float thresh_NoMove = 1f; // if less than this distance to mouse, don't move
    float thresh_SlowMove = 2f; // if less than this distance to mouse, move slowly

    //state
    Vector3 dir = Vector3.zero;
    float dist = 0;

    void Start()
    {
        input = GetComponent<JammerInput>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNavData();
        MoveTowardsMouse();
        CheckForReversal();
    }


    private void UpdateNavData()
    {
        dir = (input.MousePos - transform.position);
        dist = dir.magnitude;
    }

    private void MoveTowardsMouse()
    {
        if (dist < thresh_NoMove)
        {
            return;
        }
        if (dist < thresh_SlowMove)
        {
            rb.AddForce(dir.normalized * moveForce/3f);
        }
        else
        {
            rb.AddForce(dir.normalized * moveForce);
        }
        
    }
    private void CheckForReversal()
    {
        if (input.MousePos.x > transform.position.x && dist > thresh_SlowMove)
        {
            transform.localScale = new Vector2(1, 1);
            Debug.Log("facing right");
        }
        if (input.MousePos.x < transform.position.x && dist > thresh_SlowMove)
        {
            transform.localScale = new Vector2(-1, 1);
            Debug.Log("facing left");
        }
    }
}
