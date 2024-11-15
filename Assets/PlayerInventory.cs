using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<int> keys = new List<int>(); // Lista zdobytych kluczy

    public void AddKey(int keyID)
    {
        if (!keys.Contains(keyID))
        {
            keys.Add(keyID);
            Debug.Log("Dodano klucz o ID: " + keyID);
        }
    }

    public bool HasKey(int keyID)
    {
        return keys.Contains(keyID);
    }

    public void UseKey(int keyID, DoorController door)
    {
        if (HasKey(keyID))
        {
            door.TryOpenDoor(keyID);
        }
        else
        {
            Debug.Log("Gracz nie ma odpowiedniego klucza!");
        }
    }

    public List<int> GetKeys()
    {
        return keys;
    }
}
