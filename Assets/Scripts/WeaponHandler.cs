using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    WeaponAnimationHandler _wah;
    InputController _ic;
    WeaponController _wc;

    private void Awake()
    {
        _wah = GetComponent<WeaponAnimationHandler>();
        _ic = FindObjectOfType<InputController>();
        _ic.OnPrimaryFireDown += ShootWeapon_Debug;
        _wc = _ic.GetComponent<WeaponController>();
    }

    private void ShootWeapon_Debug()
    {
        Vector2 dir = _ic.MouseTarget - transform.position;
        _wc.CreateWeapon(_wah.GetDisplayedMuzzlePosition(), dir,
            WeaponController.WeaponType.Cannon, null, 1f);
    }
}
