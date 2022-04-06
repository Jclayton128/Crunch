using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [SerializeField] Image[] weapons = null;

    //settings
    Color selected = Color.white;
    Color unselected = new Color(1, 1, 1, 0.3f);

    //state

    // Start is called before the first frame update
    void Start()
    {

    }      

    public void UpdateWeaponSelectionUI(int selectedWeaponIndex)
    {
        foreach (var weapon in weapons)
        {
            weapon.color = unselected;
        }
        weapons[selectedWeaponIndex].color = selected;
    }

    public void PopulateWeaponIcons((Sprite[], Color[]) sprites)
    {
        if (sprites.Item1.Length < weapons.Length)
        {
            Debug.Log("More icons than slots to display");
        }
        if (weapons.Length < sprites.Item1.Length)
        {
            Debug.Log("More slots thatn icons to fill with");
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].sprite = sprites.Item1[i];
            weapons[i].color = sprites.Item2[i];
        }
    }


}
