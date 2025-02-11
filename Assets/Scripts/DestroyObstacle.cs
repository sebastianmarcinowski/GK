using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDestroy : MonoBehaviour
{
    [SerializeField] private Transform pfBrokenObstacle;
    public float interactionDist = 9.9f;
    private GameObject currentInteractionText; // Reference to the currently active interaction text
    public GameObject player;
    private WrenchCrateController wrenchController;
    public GameObject UIText;
    private InventoryUI inventoryUI;
    void Start()
    {
        wrenchController = player.GetComponent<WrenchCrateController>();
        inventoryUI = UIText.GetComponent<InventoryUI>();
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Reset the interaction text if no object is hit
        if (Physics.Raycast(ray, out hit, interactionDist))
        {
            if (hit.collider.gameObject.CompareTag("VentTag"))
            {
                // Get the obstacle object and its interaction text
                GameObject obstacle = hit.collider.gameObject;
                Transform canvasTransform = obstacle.transform.Find("CanvasObstacle");

                if (canvasTransform != null)
                {
                    currentInteractionText = canvasTransform.Find("obstacleInteractionText")?.gameObject;
                }

                // Enable interaction text
                if (currentInteractionText != null)
                {
                    currentInteractionText.SetActive(true);
                }

                // Check for interaction input
                if (Input.GetKeyDown(KeyCode.E) && wrenchController!=null && wrenchController.getWrench == true)
                {
                    Transform brokenObstacle = Instantiate(pfBrokenObstacle, obstacle.transform.position, obstacle.transform.rotation);
                    StartCoroutine(DestroyAfterDelay(brokenObstacle.gameObject, 3.5f));
                    Destroy(obstacle); // Destroy the original obstacle
                    currentInteractionText.SetActive(false); // Hide interaction text
                }
                else if (Input.GetKeyDown(KeyCode.E) && wrenchController != null && wrenchController.getWrench == false)
                {
                    //Debug.Log("Nie posiadasz klucza");
                    inventoryUI.wrenchAlert();
                }
            }
        }
        else
        {
            // Disable interaction text if no obstacle is detected
            if (currentInteractionText != null)
            {
                currentInteractionText.SetActive(false);
                currentInteractionText = null;
            }
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
