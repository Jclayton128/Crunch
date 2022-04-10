using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeaponHandler : MonoBehaviour
{

    InputController _ic;
    [SerializeField] SpriteRenderer _weaponSR = null;
    [SerializeField] Transform _weaponTransform = null;
    [SerializeField] Sprite[] _rifleSprites_Left = null;
    CrunchMovement _cm;
    //[SerializeField] Sprite[] _rifleSprites_Right = null;

    //settings
    [SerializeField] Transform[] _weaponPivots = null;
    float _maxPitch = 60f;
    float _minPitch = -60f;
    float _turnRate = 90f; // degrees per second;
    float _sectorSize;

    //state
    Vector3 _dir = Vector2.zero;
    public float _angleDesired = 0;
    float angleActual = 0;
    public int _angleIndex = 0;

    private void Awake()
    {
        _ic = FindObjectOfType<InputController>();
        _cm = GetComponent<CrunchMovement>();
        _cm.OnBeginReversal += SuppressWeaponryDuringReversal;
        _cm.OnCompleteReversal += EnableWeaponryAfterReversal;
    }

    private void Start()
    {
        _sectorSize = (Mathf.Abs(_minPitch) + _maxPitch) / _rifleSprites_Left.Length;
    }

    private void Update()
    {
        CalculateAngle();
        AdjustSprite();
        AdjustMuzzlePoint();
    }
    private void CalculateAngle()
    {
        _dir = _ic.MouseTarget - transform.position;

        if (_ic.IsFacingRight)
        {
            _angleDesired = Vector3.SignedAngle(transform.right, _dir, Vector3.forward);
        }
        else
        {
            _angleDesired = -1 * Vector3.SignedAngle(transform.right * -1, _dir, Vector3.forward);
        }

    }

    private void AdjustSprite()
    {
        _angleIndex = Mathf.RoundToInt((_angleDesired - 15f - _minPitch) / _sectorSize);
        _angleIndex = Mathf.Clamp(_angleIndex, 0, _rifleSprites_Left.Length - 1);
        _weaponSR.sprite = _rifleSprites_Left[_angleIndex];
        //if (_ic.IsFacingRight)
        //{

        //}
        //else
        //{
        //    _angleIndex = Mathf.RoundToInt((_angleDesired - 15f - _minPitch) / _sectorSize);
        //    _angleIndex = Mathf.Clamp(_angleIndex, 0, _rifleSprites_Left.Length - 1);
        //    _sr.sprite = _rifleSprites_Left[_angleIndex];
        //}
    }
    private void AdjustMuzzlePoint()
    {
        //throw new NotImplementedException();
    }

    public void ShiftPivotPoint(int index)
    {
        //Debug.Log($"shifting pivot to {index}");
        _weaponTransform.position = _weaponPivots[index].position;
        //Adjust _weaponTransform rotation or muzzle point;
    }

    private void SuppressWeaponryDuringReversal()
    {
        Debug.Log("suppress weapons");
        _weaponSR.enabled = false;
    }


    private void EnableWeaponryAfterReversal()
    {
        _weaponSR.enabled = true;        
    }

}
