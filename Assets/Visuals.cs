using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Visuals : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFacing();
    }

    private void UpdateFacing()
    {
        if (rb)
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
                //sr.flipX = false;
            }
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
                //sr.flipX = true;
            }
        }
    }
}
