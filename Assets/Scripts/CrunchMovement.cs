using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrunchMovement : MonoBehaviour
{
    InputController _ic;
    Rigidbody2D _rb;
    Animator _anim;
    CinemachineImpulseSource _cis;
    public Action OnBeginReversal;
    public Action OnCompleteReversal;

    //settings
    [SerializeField] float _moveForce;
    float _stopSpeedThresh = 0.2f;
    float _walkSpeedThresh = 1.2f;

    //state
    public int _walkMode; //-1: backpedal, 0: idle, 1: walk, 2: run
    public int _gear = 1;
    public bool MustReverse { get; private set; } = false;
    public bool IsReversing { get; private set; } = false;

    //anim state
    public float _idleFrame; // 0: fore leg forward, 1: back leg forward
    public bool _foreLegForward = true;

    private void Awake()
    {
        _ic = FindObjectOfType<InputController>();
        _ic.OnReverseDirection += HandleReverseDirection;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _ic.OnShiftDown += HandleShiftMode;
        Time.timeScale = 1f;
        _cis = Camera.main.GetComponentInChildren<CinemachineImpulseSource>();
    }

    private void Update()
    {
        UpdateAnimator();
    }

    private void HandleReverseDirection()
    {
        _anim.SetBool("MustReverse", true);
        MustReverse = true;
    }

    public void BeginReversing()
    {
        IsReversing = true;
        OnBeginReversal?.Invoke();
    }

    public void CompleteReversal() // Called via AnimationEvent
    {
        _anim.SetBool("MustReverse", false);
        OnCompleteReversal?.Invoke();
        MustReverse = false;
        IsReversing = false;
        UpdateTransformToFaceDirection();
    }

    private void UpdateAnimator()
    {
        if (MustReverse) return;
        if (_ic.IsFacingRight)
        {
            if (_ic.MoveSignal_Horizontal < 0)
            {
                if (_walkMode != -1)
                {
                    _anim.SetFloat("Speed", -1);
                    //Debug.Log("backpedal speed -1");
                }
                _walkMode = -1;
                return;
            }
            if (Mathf.Abs(_ic.MoveSignal_Horizontal) < Mathf.Epsilon)
            {
                if (_walkMode != 0)
                {
                    _anim.SetFloat("Speed", 0);
                }
                _walkMode = 0;
                return;
            }
            if (_rb.velocity.x <= _walkSpeedThresh)
            {
                if (_walkMode != 1)
                {
                    _anim.SetFloat("Speed", 1);
                }
                _walkMode = 1;
                return;
            }
            if (_rb.velocity.x > _walkSpeedThresh)
            {
                if (_walkMode != 2)
                {
                    _anim.SetFloat("Speed", 2);
                }
                _walkMode = 2;
                return;
            }
        }
        if (!_ic.IsFacingRight)
        {
            if (_ic.MoveSignal_Horizontal > 0)
            {
                if (_walkMode != -1)
                {
                    _anim.SetFloat("Speed", -1);
                }
                _walkMode = -1;
                return;
            }
            if (Mathf.Abs(_ic.MoveSignal_Horizontal) < Mathf.Epsilon)
            {
                if (_walkMode != 0)
                {
                    _anim.SetFloat("Speed", 0);
                }
                _walkMode = 0;
                return;
            }
            if (_rb.velocity.x >= -1 * _walkSpeedThresh)
            {
                if (_walkMode != 1)
                {
                    _anim.SetFloat("Speed", 1);
                }
                _walkMode = 1;
                return;
            }
            if (_rb.velocity.x < -1 * _walkSpeedThresh)
            {
                if (_walkMode != 2)
                {
                    _anim.SetFloat("Speed", 2);
                }
                _walkMode = 2;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateTransformToFaceDirection()
    {
        if (_ic.IsFacingRight)
        {
            Debug.Log("negative x transform");
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (!_ic.IsFacingRight)
        {
            Debug.Log("positive x transform");
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Move()
    {
        if (Mathf.Abs(_ic.MoveSignal_Horizontal) <= float.Epsilon)
        {
            _rb.drag = 5;
        }
        else
        {
            if (MustReverse) return;
            _rb.drag = 2;
            _rb.AddForce(_moveForce * _gear * _ic.MoveSignal_Horizontal * Vector2.right);
        }

    }

    private void HandleShiftMode()
    {
        if (_gear == 1)
        {
            _gear = 2;
            return;
        }
        if (_gear == 2)
        {
            _gear = 1;
            return;
        }
    }

    public void UpdateIdleFrame(float bestIdleFrame)
    {
        //Debug.Log($"called {bestIdleFrame}");
        _idleFrame = bestIdleFrame;
        _anim.SetFloat("IdleFrame", _idleFrame);
        _cis.GenerateImpulse(_rb.velocity.magnitude);
    }
}

