using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCratesController : MonoBehaviour
{
    private GameObject currentInteractionText;
    public float interactionDist = 4.0f;
    public GameObject UIText;
    private InventoryUI inventoryUI;
    private void Start()
    {
        inventoryUI = UIText.GetComponent<InventoryUI>();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.8f, (transform.forward + Vector3.down * 0.2f).normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDist))
        {
            if (hit.collider.gameObject.CompareTag("KeyCrateTag") || hit.collider.gameObject.CompareTag("CorrectCrate"))
            {
                GameObject crate = hit.collider.gameObject;
                Transform canvasTransform = crate.transform.Find("CanvasCrate");
                if (canvasTransform != null) currentInteractionText = canvasTransform.Find("crateInteractionText")?.gameObject;

                if (currentInteractionText != null) currentInteractionText.SetActive(true);

                //Podniesienie klucza do otwarcia wenta
                if (Input.GetKeyDown(KeyCode.E) && hit.collider.gameObject.CompareTag("KeyCrateTag"))
                {
                    inventoryUI.statueCrate();
                    Destroy(currentInteractionText);
                    //Debug.Log("You have key");
                }
                if (Input.GetKeyDown(KeyCode.E) && hit.collider.gameObject.CompareTag("CorrectCrate"))
                {
                    inventoryUI.statueCorrectCrate();
                    Destroy(currentInteractionText);
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
