using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int doorID;
    public PlayerInventory inventory;
    public float interactionDist;
    public GameObject interactionText;
    public string doorOpenAnim, doorCloseAnim;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactionDist)) 
        { 
            if (hit.collider.gameObject.tag == "DoorTag")
            {
                GameObject doorParent = hit.collider.transform.root.gameObject;
                Animator doorAnimation = doorParent.GetComponent<Animator>();
                interactionText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && inventory.keys.Contains(doorID))
                {
                    Open(doorParent, doorAnimation);
                }

            }
            else
            {
                interactionText.SetActive(false);
            }
        }
        else
            {
                interactionText.SetActive(false);
            }
    }
    
    

    void Open(GameObject doorParent, Animator doorAnimation)
    {
        if (doorAnimation.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnim))
        {
            doorAnimation.ResetTrigger("open");
            doorAnimation.SetTrigger("closed");
        }
        if (doorAnimation.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnim))
        {
            doorAnimation.ResetTrigger("closed");
            doorAnimation.SetTrigger("open");
        }
    }
}
