using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    GameObject crunch;
    GameObject jammer;


    void Start()
    {
        crunch = GameObject.FindGameObjectWithTag("Crunch");
        jammer = GameObject.FindGameObjectWithTag("Jammer");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((crunch.transform.position.x + jammer.transform.position.x) / 2f, 0, 0);
    }
}
