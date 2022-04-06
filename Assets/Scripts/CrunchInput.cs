using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunchInput : PlayerInput
{
    ArmMovement arm;
    WeaponController ws;

    //state
    public float MoveSignal_Horizontal { get; private set; } = 0;
    public float MoveSignal_Vertical { get; private set; } = 0;

    float timeSpaceHeld = 0;
    float doubleTapTime;

    void Start()
    {
        arm = GetComponentInChildren<ArmMovement>();

        ws = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    void Update()
    {
        ListenForKeyboardInput();

    }

    
    private void ListenForKeyboardInput()
    {
        ListenForWeaponSelect();
        if (arm)
        {
            ListenForAiming();
        }

        ListenForMoveLeft();
        ListenForMoveRight();
        ListenForNullMovement();

        ListenForFire();
        //MoveSignal_Horizontal = Input.GetAxisRaw("Horizontal");
        //MoveSignal_Vertical = Input.GetAxisRaw("Vertical");
    }

    private void ListenForWeaponSelect()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            OnScrollUpLeft?.Invoke();
            return;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            OnScrollDownRight?.Invoke();
            return;
        }
    }

    private void ListenForAiming()
    {
        if (Input.GetKey(KeyCode.W))
        {
            arm.IncreaseArmPitch();
        }
        if (Input.GetKey(KeyCode.S))
        {
            arm.DecreaseArmPitch();
        }
    }

    private void ListenForFire()
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
        if (timeSpaceHeld > 0 && !Input.GetKey(KeyCode.Space))
        {
            timeSpaceHeld = 0;
            OnFireUp?.Invoke(timeSpaceHeld);
        }
    }

    private void ListenForMoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time <= doubleTapTime)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            IsFacingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time > doubleTapTime)
        {
            //first tap
            doubleTapTime = Time.time + Preferences.GetDoublePressThreshold();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsFacingRight)
            {
                MoveSignal_Horizontal = -.3f;
            }
            if (!IsFacingRight)
            {
                MoveSignal_Horizontal = -1f;
            }
        }

    }
    private void ListenForMoveRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time <= doubleTapTime)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            IsFacingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time > doubleTapTime)
        {
            //first tap
            doubleTapTime = Time.time + Preferences.GetDoublePressThreshold();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!IsFacingRight)
            {
                MoveSignal_Horizontal = .3f;
            }
            if (IsFacingRight)
            {
                MoveSignal_Horizontal = 1f;
            }
        }

    }

    private void ListenForNullMovement()
    {
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            MoveSignal_Horizontal = 0;
        }
    }
}
