using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrunchInput : PlayerInput
{
    ArmMovement arm;
    WeaponController ws;

    //state
    public float MoveSignal_Horizontal; //*{ get; private set; } */= 0;
    public float MoveSignal_Vertical; // { get; private set; } = 0;

    float timeSpaceHeld = 0;
    float doubleTapTime;

    void Start()
    {
        arm = GetComponentInChildren<ArmMovement>();

        ws = GetComponent<WeaponController>();
    }

    // Update is called once per frame
    
}
