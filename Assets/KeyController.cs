using UnityEngine;

public class KeyController : MonoBehaviour
{
    public PlayerInventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Przycisk "E" do otwierania drzwi
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                DoorController door = hit.collider.GetComponent<DoorController>();
                if (door != null && !door.isOpen)
                {
                    inventory.UseKey(door.doorID, door);
                }
            }
        }
    }
}
