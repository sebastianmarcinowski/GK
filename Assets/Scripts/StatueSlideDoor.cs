using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueSlideDoor : MonoBehaviour
{
    private Animator animator;
    public bool isOpen = false;
    StatueMessage message;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            animator.SetTrigger("Open");
            message.isMessageActive = true;
        }
    }
}
