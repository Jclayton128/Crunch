using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] Transform muzzleTransform = null;
    PlayerInput input;

    //settings
    float timeBetweenBullets = 0.25f;
    float bulletSpeed = 20f;

    //state
    bool shouldBeFiring = false;
    float timeForNextShot;

    private void Start()
    {
        input = GetComponentInParent<PlayerInput>();
        input.OnLMBDown += BeginFiring;
        input.OnLMBUp += CeaseFiring;
    }

    private void BeginFiring()
    {
        shouldBeFiring = true;
    }

    private void CeaseFiring(float timeHeld)
    {
        shouldBeFiring = false;
        timeForNextShot = Time.time + timeBetweenBullets;
    }

    private void Update()
    {
        if (shouldBeFiring && Time.time > timeForNextShot)
        {
            FireBullet();
            timeForNextShot = Time.time + timeBetweenBullets;
        }
    }
    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, muzzleTransform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bulletSpeed;
        bullet.GetComponent<DamageDealer>().SetDamage(1);
        Destroy(bullet, 1f);       
    }
}
