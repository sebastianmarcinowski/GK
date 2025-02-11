using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredWiresbehaviour : MonoBehaviour
{
    bool mouseDown = false;
    public PoweredWireStats powWireStats;
    LineRenderer line;

    private void Start()
    {
        powWireStats = gameObject.GetComponent<PoweredWireStats>();
        line = gameObject.GetComponent<LineRenderer>(); 
    }
    private void Update()
    {
        moveWire();
        line.SetPosition(3, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        line.SetPosition(2, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
    }
    private void OnMouseDown()
    {
        mouseDown = true;
    }
    private void OnMouseUp()
    {
        mouseDown = false;
        if (!powWireStats.connected)
        {
            gameObject.transform.position = powWireStats.startPosition;
        }
        if(powWireStats.connected)
        {
            gameObject.transform.position = powWireStats.connectedPosition;
        }
    }
    private void OnMouseOver()
    {
        powWireStats.isMovable = true;
    }
    private void OnMouseExit()
    {
        if (!powWireStats.isMoving) powWireStats.isMovable = false;
    }
    void moveWire()
    {
        if(mouseDown && powWireStats.isMovable == true)
        {
            powWireStats.isMoving = true;
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mouseX, mouseY, 1));
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, transform.parent.transform.position.z);

        }
        else
        {
            powWireStats.isMoving = false;
        }
    } 
}
