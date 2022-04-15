using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBrain : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    GameObject _crashGO;
    private enum MoveMode { Idle, Moving, ShootLow, ShootHigh}

    //Settings
    [SerializeField] float _moveSpeed = 3f;
    float _closeEnoughToStopMoving = 0.1f;
    float _closeEnoughToKeepSameTargetX = 1.0f;

    //state
    public bool IsFacingRight { get; private set; } = true;
    public float _distDestToCrash;
    public float _distToTargetX;
    public float _targetX;
    [SerializeField] MoveMode _moveMode;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _crashGO = GameObject.FindGameObjectWithTag("Crunch");
    }

    private void Update()
    {
        UpdateNavData();
        UpdateAnimator();
        UpdateFacing();
    }
    
    private void UpdateNavData()
    {
        _distDestToCrash = _crashGO.transform.position.x - _targetX;
        if (Mathf.Abs(_distDestToCrash) > _closeEnoughToKeepSameTargetX)
        {
            _targetX = _crashGO.transform.position.x + 
                (UnityEngine.Random.Range(_closeEnoughToKeepSameTargetX/2f, 
                _closeEnoughToKeepSameTargetX)  * Mathf.Sign(UnityEngine.Random.Range(-1,1)));
        }
        _distToTargetX = _targetX - transform.position.x;

    }
    private void UpdateAnimator()
    {
        _anim.SetFloat("Blend", (float)_moveMode);   
    }


    private void FixedUpdate()
    {

        if (Mathf.Abs(_distToTargetX) > _closeEnoughToStopMoving)
        {
            _moveMode = MoveMode.Moving;
            _rb.AddForce(_moveSpeed * Mathf.Sign(_distToTargetX) * Vector2.right);
        }
        else
        {
            _moveMode = MoveMode.Idle;
        }

    }

    private void UpdateFacing()
    {
        if (_distToTargetX < 0 && IsFacingRight)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IsFacingRight = false;
        }
        if (_distToTargetX > 0 && !IsFacingRight)
        {
            transform.localScale = Vector3.one;
            IsFacingRight = true;
        }
    }
}
