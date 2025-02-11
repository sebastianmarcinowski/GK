using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WireGameController : MonoBehaviour
{
    public List<PoweredWireStats> poweredWires;
    public List<UnpoweredWireStats> unpoweredWires;
    private bool gameCompleted = false;
    private void Start()
    {
        poweredWires = new List<PoweredWireStats>(FindObjectsOfType<PoweredWireStats>());
        unpoweredWires = new List<UnpoweredWireStats>(FindObjectsOfType<UnpoweredWireStats>());
    }

    // Sprawdza, czy wszystkie kable s¹ po³¹czone poprawnie
    public bool AreAllWiresConnected()
    {
        foreach (var poweredWire in poweredWires)
        {
            if (!poweredWire.connected)
            {
                return false;
            }
        }
        foreach (var unpoweredWire in unpoweredWires)
        {
            if (!unpoweredWire.isConnected)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (!gameCompleted && AreAllWiresConnected())
        {
            gameCompleted = true; // Zapobiega wielokrotnemu wywo³aniu
            EventManager.WireGameCompleted();
            SceneManager.LoadScene(1);
        }
    }
}
