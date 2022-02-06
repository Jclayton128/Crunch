using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float MoveSignal_Horizontal { get; private set; } = 0;
    public float MoveSignal_Vertical { get; private set; } = 0;

    public Vector3 MousePos; //{ get; private set; } = Vector2.zero;

    public Action OnLMBDown;
    public Action<float> OnLMBUp;
    public Action OnRMBDown;
    public Action<float> OnRMBUp;

    //state
    float timePressed_LMB = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ListenForKeyboardInput();
        ListenForMousePosition();
        ListenForMouseClick();
    }

    private void ListenForMouseClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnLMBDown?.Invoke();
            Debug.Log("LMB down");
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            timePressed_LMB += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnLMBUp?.Invoke(timePressed_LMB);
            Debug.Log("LMB up");
            timePressed_LMB = 0;
        }
    }

    private void ListenForMousePosition()
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
