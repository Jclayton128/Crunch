using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSystem")]
public class WeaponSystem : ScriptableObject
{
    [SerializeField] Sprite weaponIcon;
    [SerializeField] Color iconColor = Color.white;
    [SerializeField] GameObject weaponProjectilePrefab = null;
    [SerializeField] float timeBetweenProjectiles = 1f;
    [SerializeField] bool isChargeable = false;
    [SerializeField] bool isContinuousFire = false;
    [SerializeField] bool isHoming = false;
    [SerializeField] float weaponLifetime = 10f;

    //state
    float timeForNextProjectile;
    bool shouldBeFiring;
    Transform muzzle;

    
    public void ResetWeaponSystem()
    {
        timeForNextProjectile = 0;
        shouldBeFiring = false;
        muzzle = null;
    }       

    public void HandleFireDown(Transform muzzle)
    {
        this.muzzle = muzzle;
        shouldBeFiring = true;
    }

    public void HandleFireUp(float chargeTime, Vector3 targetPosition)
    {
        shouldBeFiring = false;
        if (isHoming)
        {
            CreateWeapon(muzzle, targetPosition, weaponLifetime, chargeTime);
        }
        if (isChargeable)
        {
            CreateWeapon(muzzle, targetPosition, weaponLifetime, chargeTime);
        }
    }
    public void HandleFireUp(float chargeTime, Transform targetTransform)
    {
        shouldBeFiring = false;
        if (isHoming)
        {
            CreateWeapon(muzzle, targetTransform, weaponLifetime, chargeTime);
        }
        if (isChargeable)
        {
            CreateWeapon(muzzle, targetTransform, weaponLifetime, chargeTime);
        }
    }

    public void HandleFiringPulse()
    {
        if (isContinuousFire && shouldBeFiring && Time.time >= timeForNextProjectile)
        {
            CreateWeapon(muzzle, Vector3.zero, weaponLifetime, 1);
        }
    }

    private void CreateWeapon(Transform muzzle, Transform targetTransform, float lifetime, float chargePower)
    {
        GameObject weapon = Instantiate(weaponProjectilePrefab, muzzle.position, muzzle.rotation);
        weapon.GetComponent<WeaponBehavior>().Initialize(targetTransform, lifetime, chargePower);
        timeForNextProjectile = Time.time + timeBetweenProjectiles;
    }

    private void CreateWeapon(Transform muzzle, Vector3 targetPosition, float lifetime, float chargePower)
    {
        GameObject weapon = Instantiate(weaponProjectilePrefab, muzzle.position, muzzle.rotation);
        weapon.GetComponent<WeaponBehavior>().Initialize(targetPosition, lifetime, chargePower);
        timeForNextProjectile = Time.time + timeBetweenProjectiles;
    }

    public Sprite GetIcon()
    {
        return weaponIcon;
    }

    public Color GetColor()
    {
        return iconColor;
    }
}
