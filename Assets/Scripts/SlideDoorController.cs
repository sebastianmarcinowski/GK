using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    public GameObject UIText;
    private InventoryUI inventoryUI;
    private void Start()
    {
        //inventoryUI = UIText.GetComponent<InventoryUI>();
        animator = GetComponent<Animator>();
        if (EventManager.IsWireGameCompleted())
        {
            OpenDoor();
        }
        else
        {
            EventManager.OnWireGameComplete += OpenDoor; // Subskrybuj event
        }
    }


    private void OnDestroy()
    {
        EventManager.OnWireGameComplete -= OpenDoor;
    }

    public void OpenDoor()
    {
        Debug.Log("SlideDoorController: OpenDoor called");
        if (!isOpen && animator != null)
        {
            animator.SetTrigger("OpenSlideDoor");
            isOpen = true;
            inventoryUI.wireGameAlert();
        }
    }

}
