using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Collider : MonoBehaviour
{
    // Reference to the InventoryUI script
    public InventoryUI inventoryUI;

    // This function is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Make sure the collider is the player (You can tag the player or check its name)
        if (other.CompareTag("Player"))
        {
            // Call the OpenInventory function from InventoryUI script
            if (inventoryUI != null)
            {
                inventoryUI.statuesEnter();
            }
        }
    }
}
