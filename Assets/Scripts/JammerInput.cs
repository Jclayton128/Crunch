using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JammerInput : PlayerInput
{
    public Vector3 MousePos; //{ get; private set; } = Vector2.zero;

    //state
    float timePressed_LMB = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ListenForMousePosition();
        ListenForMouseClick();
    }

    private void ListenForMouseClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnFireDown?.Invoke();
            //Debug.Log("LMB down");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timePressed_LMB += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnFireUp?.Invoke(timePressed_LMB);
            //Debug.Log("LMB up");
            timePressed_LMB = 0;
        }
    }

    private void ListenForMousePosition()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            MousePos = ray.GetPoint(distance);
        }
    }

}
