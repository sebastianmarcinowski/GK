using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCrate : MonoBehaviour
{
    private GameObject currentInteractionText;
    public float interactionDist = 2.0f;
    public bool getKey = false;
    public GameObject UIText;
    private InventoryUI inventoryUI;
    private void Start()
    {
        inventoryUI = UIText.GetComponent<InventoryUI>();
    }
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDist))
        {
            if (hit.collider.gameObject.CompareTag("MazeCrateTag"))
            {
                GameObject crate = hit.collider.gameObject;
                Transform canvasTransform = crate.transform.Find("CanvasCrate");
                if (canvasTransform != null) currentInteractionText = canvasTransform.Find("crateInteractionText")?.gameObject;

                if (currentInteractionText != null) currentInteractionText.SetActive(true);

                //Podniesienie klucza do otwarcia wenta
                if (Input.GetKeyDown(KeyCode.E))
                {
                    getKey = true;
                    inventoryUI.mazeKeyAlert();
                    Destroy(currentInteractionText);
                    //Debug.Log("You have key");
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
