using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBrain : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;

    //settings
    float _timeBetweenJumps = 3f;
    [SerializeField] float _maxJumpPower = 6f;
    float _jumpChargeTime = 2f;

    //state
    public float _targetX;
    float _timeForNextCharge;
    float _jumpStrength;
    float _jumpInaccuracy = 1f;
    Vector2 _jumpDir_right = new Vector2(1,1);
    Vector2 _jumpDir_left = new Vector2(-1, 1);
    float _timeForNextJump;
    bool _isReadyToJump = false;
    public bool _isAirborne = false;

    protected void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _timeForNextJump = GetRandomNextChargeTime();
    }

    private void Update()
    {
        UpdateNavigationToNearestBuilding();
        UpdateJumpTiming();

    }

    private void UpdateNavigationToNearestBuilding()
    {
       _targetX = FindXPositionOfNearestBuilding();
    }

    private float FindXPositionOfNearestBuilding()
    {
        float dist = Mathf.Infinity;
        GameObject closestBuilding = null;
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (var building in buildings)
        {
            float curdist = (transform.position.x - building.transform.position.x);
            if (curdist < dist)
            {
                dist = curdist;
                closestBuilding = building;
                continue;
            }
        }
        return closestBuilding.transform.position.x;
    }

    private void UpdateJumpTiming()
    {
        if (_isAirborne) return;
        if (Time.time >= _timeForNextCharge)
        {
            _timeForNextJump = Time.time + _jumpChargeTime;
            _timeForNextCharge = Mathf.Infinity;
        }
        if (Time.time >= _timeForNextJump)
        {
            _isReadyToJump = true;
            _timeForNextJump = Mathf.Infinity;
            _timeForNextCharge = GetRandomNextChargeTime();
        }
    }

    private void FixedUpdate()
    {
        if (_isReadyToJump)
        {
            JumpSemiRandomly();
            _isReadyToJump = false;
        }
    }

    private void JumpSemiRandomly()
    {
        _jumpStrength = UnityEngine.Random.Range(0.2f, _maxJumpPower);

        if (_targetX > transform.position.x)
        {
            _rb.AddForce(_jumpDir_right * _jumpStrength, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(_jumpDir_left * _jumpStrength, ForceMode2D.Impulse);
        }
    }

    private float GetRandomNextChargeTime()
    {
        return Time.time + _timeBetweenJumps +
            UnityEngine.Random.Range(-_timeBetweenJumps / 2f, _timeBetweenJumps / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            _anim.SetBool("IsAirborne", false);
            _isAirborne = false;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            _anim.SetBool("IsAirborne", true);
            _isAirborne = true;

        }
    }
}
