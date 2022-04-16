using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationHandler : MonoBehaviour
{

    InputController _ic;
    [SerializeField] SpriteRenderer _weaponSR = null;
    [SerializeField] Transform _weaponTransform = null;
    [SerializeField] Sprite[] _rifleSprites_Left = null;
    [SerializeField] ParticleSystem _ps = null;
    [SerializeField] float[] _particleOffsets = null;
    CrunchMovement _cm;

    //settings
    [SerializeField] Transform[] _weaponPivots = null;
    float _maxPitch = 60f;
    float _minPitch = -60f;
    float _sectorSize;

    //state
    public Transform CurrentWeaponPivot { get; private set; }
    float _displayedMuzzleAngle;
    Vector3 _dir = Vector2.zero;
    float _angleDesired = 0;
    int _angleIndex = 0;

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

        switch (_angleIndex)
        {
            case 0:
                _displayedMuzzleAngle = 45f;
                break;

            case 1:
                _displayedMuzzleAngle = 22.5f;
                break;

            case 2:
                _displayedMuzzleAngle = 0f;
                break;

            case 3:
                _displayedMuzzleAngle = -22.5f;
                break;

            case 4:
                _displayedMuzzleAngle = -45f;
                break;


        }
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
    private void SuppressWeaponryDuringReversal()
    {
        _weaponSR.enabled = false;
    }


    private void EnableWeaponryAfterReversal()
    {
        _weaponSR.enabled = true;        
    }

    #region Animation Public Methods
    public void ShiftPivotPoint(int index)
    {
        //Debug.Log($"shifting pivot to {index}");
        _weaponTransform.position = _weaponPivots[index].position;
        CurrentWeaponPivot = _weaponPivots[index];

        //Adjust _weaponTransform rotation or muzzle point;
    }

    public void HandleFootfall(int index)
    {
        if (!_ic.IsFacingRight)
        {
            _ps.transform.position = new Vector2(transform.position.x + _particleOffsets[index], 0);
        }
        if (_ic.IsFacingRight)
        {
            _ps.transform.position = new Vector2(transform.position.x - _particleOffsets[index], 0);
        }
        
        if (index == 0)
        {
            _ps.Emit(20);
        }
        else
        {
            _ps.Emit(7);
        }


    }

    #endregion

    #region Helpers
    public Vector3 GetDisplayedMuzzlePosition()
    {
        if (_ic.IsFacingRight)
        {
            Vector3 muzzleOffset = new Vector2(0.6f * Mathf.Cos(_displayedMuzzleAngle * Mathf.Deg2Rad),
                -0.6f * Mathf.Sin(_displayedMuzzleAngle * Mathf.Deg2Rad));
            return muzzleOffset + CurrentWeaponPivot.position;
        }
        else
        {
            Vector3 muzzleOffset = new Vector2(0.6f * Mathf.Cos(_displayedMuzzleAngle * Mathf.Deg2Rad),
                0.6f * Mathf.Sin(_displayedMuzzleAngle * Mathf.Deg2Rad));
            return -muzzleOffset + CurrentWeaponPivot.position;
        }


    }

    #endregion

}
