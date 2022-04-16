using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Visuals : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    CrunchInput input;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<CrunchInput>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        if (input)
        {
            if (input.IsFacingRight)
            {
                transform.localScale = new Vector2(1, 1);
                //sr.flipX = false;
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
                //sr.flipX = true;
            }
        }
    }
}
