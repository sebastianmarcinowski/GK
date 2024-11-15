using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDestroy : MonoBehaviour
{

    public float interactionDist;
    public GameObject interactionText;
    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(gameObject != null)
        {
            if (Physics.Raycast(ray, out hit, interactionDist))
            {
                if (hit.collider.gameObject.tag == "VentTag")
                {
                    interactionText.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }
                else
                {
                    interactionText.SetActive(false);
                }
            }
        }
        else
        {
            interactionText.SetActive(false);
        }
    }
}
