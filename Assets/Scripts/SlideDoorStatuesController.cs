using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorStatuesController : MonoBehaviour
{
    public StatueSlideDoor statueSlideDoor;
    public GameObject UIText;
    private InventoryUI inventoryUI;
    public GameObject player;
    private MazeCrate mazeKeyController;
    public float interactionDist = 1.0f;
    private GameObject currentInteractionText;
    void Start()
    {
        inventoryUI = UIText.GetComponent<InventoryUI>();
        mazeKeyController = player.GetComponent<MazeCrate>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDist))
        {
            if (hit.collider.gameObject.CompareTag("DoorTag"))
            {
                GameObject door = hit.collider.gameObject;
                Transform canvasTransform = door.transform.Find("CanvasDoor");

                if (canvasTransform != null)
                {
                    currentInteractionText = canvasTransform.Find("doorInteractionText")?.gameObject;
                }

                if (currentInteractionText != null)
                {
                    currentInteractionText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E) && mazeKeyController != null && mazeKeyController.getKey == true)
                {
                    statueSlideDoor.isOpen = true;
                    Destroy(currentInteractionText);
                }
                else if (Input.GetKeyDown(KeyCode.E) && mazeKeyController != null && mazeKeyController.getKey == false)
                {
                    inventoryUI.mazeDoorAlert();
                }
            }
        }
        else
        {
            if (currentInteractionText != null)
            {
                currentInteractionText.SetActive(false);
                currentInteractionText = null;
            }
        }

    }
}