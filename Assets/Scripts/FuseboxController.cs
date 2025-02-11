using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuseboxController : MonoBehaviour
{
    private GameObject currentInteractionText;
    public float interactionDist = 9.9f;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionDist))
        {
            if (hit.collider.gameObject.CompareTag("WireGameTag"))
            {
                GameObject fusebox = hit.collider.gameObject;
                Transform canvasTransform = fusebox.transform.Find("CanvasFusebox");
                if (canvasTransform != null) currentInteractionText = canvasTransform.Find("fuseboxInteractionText")?.gameObject;

                if (currentInteractionText != null) currentInteractionText.SetActive(true);

                //Uruchamianie rozgrywki (kabelków)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SceneManager.LoadScene(2);
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