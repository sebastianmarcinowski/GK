using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum wireColors { blue, green, red, yellow, orange, purple };

public class PoweredWireStats : MonoBehaviour
{
    public bool isMovable = false;
    public bool isMoving = false;
    public Vector3 startPosition;
    public wireColors wireColor;
    public bool connected = false;
    public Vector3 connectedPosition;
    private void Start()
    {
        startPosition = transform.position;
    }
}
