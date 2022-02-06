using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveSignal_Horizontal { get; private set; } = 0;
    public float MoveSignal_Vertical { get; private set; } = 0;

    public Vector3 MousePos; //{ get; private set; } = Vector2.zero;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ListenForKeyboardInput();
        ListenForMouseInput();
    }

    private void ListenForMouseInput()
    {
        //MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, 0));
        float distance;
        xy.Raycast(ray, out distance);
        MousePos = ray.GetPoint(distance);
    }

    private void ListenForKeyboardInput()
    {
        MoveSignal_Horizontal = Input.GetAxisRaw("Horizontal");
        MoveSignal_Vertical = Input.GetAxisRaw("Vertical");
    }
}
