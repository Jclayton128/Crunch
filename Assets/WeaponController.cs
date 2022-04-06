using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] WeaponSystem[] weapons = null;
    WeaponSelectUI wsui;
    PlayerInput input;
    Transform muzzle;

    //state
    int currentWeapon = 0;
    bool isFiring = false;

    
    void Start()
    {
        wsui = FindObjectOfType<WeaponSelectUI>();
        wsui.PopulateWeaponIcons(GetWeaponIcons());
        wsui.UpdateWeaponSelectionUI(currentWeapon);
        input = GetComponent<PlayerInput>();
        input.OnFireDown += PassFireDown;
        input.OnFireUp += PassFireUp;
        input.OnScrollUpLeft += ScrollWeaponUpLeft;
        input.OnScrollDownRight += ScrollWeaponDownRight;
        muzzle = GetComponentInChildren<Muzzle>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PassFiring();
    }
    public void ScrollWeaponUpLeft()
    {
        currentWeapon--;
        //if (currentWeapon < 0)
        //{
        //    currentWeapon = weapons.Length - 1;
        //}
        currentWeapon = Mathf.Clamp(currentWeapon, 0, weapons.Length - 1);
        wsui.UpdateWeaponSelectionUI(currentWeapon);
    }

    public void ScrollWeaponDownRight()
    {
        currentWeapon++;
        currentWeapon = Mathf.Clamp(currentWeapon, 0, weapons.Length - 1);
        //if (currentWeapon > weapons.Length - 1)
        //{
        //    currentWeapon = 0;
        //}
        wsui.UpdateWeaponSelectionUI(currentWeapon);
    }

    public void PassFireDown()
    {
        isFiring = true;
        weapons[currentWeapon].HandleFireDown(muzzle);
    }

    public void PassFireUp(float time)
    {
        isFiring = false;
        if (input.TargetTransform)
        {
            weapons[currentWeapon].HandleFireUp(time, input.TargetTransform);
        }
        else
        {
            weapons[currentWeapon].HandleFireUp(time, input.TargetPosition);
        }

    }

    public void PassFiring()
    {
        if (isFiring)
        {
            weapons[currentWeapon].HandleFiringPulse();
        }
    }

    private (Sprite[],Color[]) GetWeaponIcons()
    {
        Sprite[] icons = new Sprite[weapons.Length];
        Color[] colors = new Color[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            icons[i] = weapons[i].GetIcon();
            colors[i] = weapons[i].GetColor();
        }
        Debug.Log($"icons: {icons.Length}");
        return (icons, colors);
    }

}
