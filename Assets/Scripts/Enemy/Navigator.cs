using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public Vector2 AimDir { get; protected set; }
    public float DesiredX { get; protected set; }
    public float DesiredY { get; protected set; }
}
