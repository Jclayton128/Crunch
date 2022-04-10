using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public Action OnPrimaryFireDown;
    public Action<float> OnPrimaryFireUp;
    public Action OnScrollUpLeft;
    public Action OnScrollDownRight;
    public Action OnShiftDown;
    public Action OnReverseDirection;

    //settings
    Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));

    //state
    public float MoveSignal_Horizontal { get; private set; } = 0;
    public float MoveSignal_Vertical { get; private set; } = 0;
    public bool IsFacingRight { get; protected set; } = false;
    public Vector3 MouseTarget { get; private set; } = Vector2.zero;

    float timeSpaceHeld = 0;
    float doubleTapTime;

    Ray ray;
    float distance;


    void Start()
    {
        
    }

    void Update()
    {
        ListenForKeyboardInput();
        ListenForMouseInput();
    }


    private void ListenForKeyboardInput()
    {
        //ListenForWeaponSelect();
        ListenForMoveLeft();
        ListenForMoveRight();
        ListenForNullMovement();

        ListenForPrimaryFire();
        ListenForShift();
        
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

    private void ListenForMouseInput()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        xy.Raycast(ray, out distance);
        MouseTarget = ray.GetPoint(distance);
    }

    private void ListenForPrimaryFire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnPrimaryFireDown?.Invoke();
            //Debug.Log("LMB down");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timeSpaceHeld += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnPrimaryFireUp?.Invoke(timeSpaceHeld);
            //Debug.Log("LMB up");
            timeSpaceHeld = 0;
        }
        if (timeSpaceHeld > 0 && !Input.GetKey(KeyCode.Space))
        {
            timeSpaceHeld = 0;
            OnPrimaryFireUp?.Invoke(timeSpaceHeld);
        }
    }
    private void ListenForShift()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift");
            OnShiftDown?.Invoke();
        }
    }

    private void ListenForMoveLeft()
    {
        if (Input.GetKey(KeyCode.D)) return; //don't accept conflicting input
        if (Input.GetKeyDown(KeyCode.A) && Time.time <= doubleTapTime && IsFacingRight)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            OnReverseDirection?.Invoke();
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
        if (Input.GetKey(KeyCode.A)) return; //don't accept conflicting input
        if (Input.GetKeyDown(KeyCode.D) && Time.time <= doubleTapTime && !IsFacingRight)
        {
            //DoubleTap
            doubleTapTime = Time.time;
            //Reverse direction
            OnReverseDirection?.Invoke();
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
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            MoveSignal_Horizontal = 0;
        }
    }
}
