using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    InputController _ic;
    [SerializeField] GameObject _genericWeaponPrefab = null;

    [Tooltip("0: Cannon, 1: Missile, 2: Artillery, 3: SmallArms")]
    [SerializeField] WeaponSystem[] _weaponOptions = null;
    public enum WeaponType { Cannon, Missile, Artillery, SmallArms};

    //settings

    //state
    List<WeaponBehavior> _activeWeapons = new List<WeaponBehavior>();
    Queue<WeaponBehavior> _pooledWeapons = new Queue<WeaponBehavior>();

    private void Awake()
    {
        _ic = GetComponent<InputController>();

    }

    public GameObject CreateWeapon(Vector3 muzzleLocation, Vector3 initialVector,
        WeaponType weaponType, Transform targetTransform, float power)
    {
        WeaponBehavior newWeapon;
        if (_pooledWeapons.Count == 0)
        {
            newWeapon = Instantiate(_genericWeaponPrefab).GetComponent<WeaponBehavior>();
            newWeapon.Initialize(this);
        }
        else
        {
            newWeapon = _pooledWeapons.Dequeue();
            newWeapon.gameObject.SetActive(true);
        }

        _activeWeapons.Add(newWeapon);
        newWeapon.transform.position = muzzleLocation;
        newWeapon.transform.rotation = Quaternion.LookRotation(Vector3.forward, initialVector);
        IntializeNewWeapon(weaponType, newWeapon);
        return newWeapon.gameObject;
    }


    public void ReturnWeapon(WeaponBehavior completedWeapon)
    {
        _pooledWeapons.Enqueue(completedWeapon);
        _activeWeapons.Remove(completedWeapon);
        completedWeapon.gameObject.SetActive(false);
    }

    private void IntializeNewWeapon(WeaponType weaponType, WeaponBehavior newWeapon)
    {
        WeaponSystem system = _weaponOptions[(int)weaponType];
        
        // Update commons stats        
        newWeapon.Lifetime = system.Lifetime;
        newWeapon.WeaponSprite = system.WeaponSprite;

        // Update specific stats
        switch (weaponType)
        {
            case WeaponType.Cannon:
                newWeapon.WeaponSprite = system.WeaponSprite;
                newWeapon.Speed = system.Speed;
                newWeapon.Damage = system.Damage;
                break;
        }
        newWeapon.SetAsNewWeapon();
    }
}
