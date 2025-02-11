using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpoweredWireBehaviour : MonoBehaviour
{
    UnpoweredWireStats unpowWireStat;
    
    // Start is called before the first frame update
    void Start()
    {
        unpowWireStat = gameObject.GetComponent<UnpoweredWireStats>();
    }

    // Update is called once per frame
    void Update()
    {
        lightManagement();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PoweredWireStats>())
        {
            PoweredWireStats powWireStats = collision.GetComponent<PoweredWireStats>();
            if (powWireStats.wireColor == unpowWireStat.wireColor)
            {
                powWireStats.connected = true;
                unpowWireStat.isConnected = true;
                powWireStats.connectedPosition = gameObject.transform.position;
                FindObjectOfType<WireGameController>().AreAllWiresConnected();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PoweredWireStats powWireStats = collision.GetComponent<PoweredWireStats>();
        if(powWireStats.wireColor == unpowWireStat.wireColor)
        {
            powWireStats.connected = false;
            unpowWireStat.isConnected = false;
            FindObjectOfType<WireGameController>().AreAllWiresConnected();
        }
    }
    void lightManagement()
    {
        if (unpowWireStat.isConnected)
        {
            unpowWireStat.poweredLight.SetActive(true);
            unpowWireStat.unpoweredLight.SetActive(false);
        }
        else
        {
            unpowWireStat.poweredLight.SetActive(false);
            unpowWireStat.unpoweredLight.SetActive(true);
        }
    }
}
