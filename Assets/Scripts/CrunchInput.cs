using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunchInput : MonoBehaviour
{
    public float MoveSignal_Horizontal { get; private set; } = 0;
    public float MoveSignal_Vertical { get; private set; } = 0;

    public Action OnFireDown;
    public Action<float> OnFireUp;

    //state
    public bool IsFacingRight { get; private set; } = false;
    float timeSpaceHeld = 0;
    float doubleTapTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ListenForKeyboardInput();

    }

    
    private void ListenForKeyboardInput()
    {
        ListenForMoveLeft();
        ListenForMoveRight();
        ListenForNullMovement();
        ListenForWeaponry();
        //MoveSignal_Horizontal = Input.GetAxisRaw("Horizontal");
        //MoveSignal_Vertical = Input.GetAxisRaw("Vertical");
    }


    private void ListenForWeaponry()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFireDown?.Invoke();
            //Debug.Log("LMB down");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            timeSpaceHeld += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnFireUp?.Invoke(timeSpaceHeld);
            //Debug.Log("LMB up");
            timeSpaceHeld = 0;
        }
    }

    private void ListenForMoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.A) && Time.time <= doubleTapTime)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            IsFacingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && Time.time > doubleTapTime)
        {
            //first tap
            doubleTapTime = Time.time + Preferences.GetDoublePressThreshold();
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (IsFacingRight)
            {
                MoveSignal_Horizontal = -.5f;
            }
            if (!IsFacingRight)
            {
                MoveSignal_Horizontal = -1f;
            }
        }

    }
    private void ListenForMoveRight()
    {
        if (Input.GetKeyDown(KeyCode.D) && Time.time <= doubleTapTime)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            IsFacingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && Time.time > doubleTapTime)
        {
            //first tap
            doubleTapTime = Time.time + Preferences.GetDoublePressThreshold();
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!IsFacingRight)
            {
                MoveSignal_Horizontal = .5f;
            }
            if (IsFacingRight)
            {
                MoveSignal_Horizontal = 1f;
            }
        }

    }

    private void ListenForNullMovement()
    {
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            MoveSignal_Horizontal = 0;
        }
    }
}
