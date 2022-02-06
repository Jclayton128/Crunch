using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    CrunchInput input;

    //settings
    float maxPitch = 60f;
    float minPitch = -60f;
    float turnRate = 90f; // degrees per second;

    //state
    Vector3 dir = Vector2.zero;
    public float angleDesired = 0;
    public float angleActual = 0;

    void Start()
    {
        input = GetComponentInParent<CrunchInput>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateArm();
    }

    public void IncreaseArmPitch()
    {
        angleActual = Mathf.MoveTowards(angleActual, maxPitch, turnRate * Time.deltaTime);
    }

    public void DecreaseArmPitch()
    {
        angleActual = Mathf.MoveTowards(angleActual, minPitch, turnRate * Time.deltaTime);
    }

    private void RotateArm()
    {
        //angleDesired = Vector3.SignedAngle(Vector3.right * transform.parent.localScale.x, dir, Vector3.forward) * transform.parent.localScale.x;
        //angleDesired = Mathf.Clamp(angleDesired, minPitch, maxPitch);
        //angleActual = Mathf.MoveTowards(angleActual, minPitch, turnRate * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, 0, angleActual);
    }
}
