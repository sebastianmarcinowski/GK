using System;
using UnityEngine;

public static class EventManager
{
    public static Action OnWireGameComplete;
    private static bool wireGameCompleted = false; // Flaga stanu

    public static void WireGameCompleted()
    {
        wireGameCompleted = true;
        OnWireGameComplete?.Invoke();
    }

    public static bool IsWireGameCompleted()
    {
        return wireGameCompleted;
    }
}
