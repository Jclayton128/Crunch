using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public Action OnFireDown;
    public Action<float> OnFireUp;
    public Action OnScrollUpLeft;
    public Action OnScrollDownRight;
    public bool IsFacingRight { get; protected set; } = false;
    public Transform TargetTransform { get; protected set; }
    public Vector3 TargetPosition { get; protected set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
