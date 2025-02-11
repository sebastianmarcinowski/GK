using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rg;
    private Animator animator;

    // Jump Variables
    int doubleJumpCheck = 2;
    private int jumpCounter = 1;
    public LayerMask floorLayer;
    private BoxCollider boxCollider;
    RaycastHit hit;
    float groundRadius = 0.05f;
    private bool onGround;
    private bool wasOnGround;

    // WASD Movement Variables
    private float horizontalInput;
    private float forwardInput;
    public float rotationSpeed = 5f;

    // Crouch and Crawl Variables
    private bool isSneaking;
    public bool isCrawling;
    private Vector3 crawlPosition, startPosition;
    private float originalScale;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        floorLayer = LayerMask.GetMask("FloorLayer");
        boxCollider = GetComponent<BoxCollider>();

        startPosition = transform.position;
        originalScale = transform.localScale.y;
        crawlPosition = new Vector3(0.0f, -3.0f, 0.0f);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Jump Logic
        onGround = Physics.Raycast(transform.position, -Vector3.up, out hit, boxCollider.bounds.extents.y + groundRadius, floorLayer);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < doubleJumpCheck && !isCrawling && !isSneaking)
        {
            jumpCounter++;
            animator.SetTrigger("Jump");
            rg.velocity = new Vector3(rg.velocity.x, -4.2f, rg.velocity.z);
            rg.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        if (onGround && !wasOnGround)
        {
            jumpCounter = 1;
        }

        wasOnGround = onGround;

        // Movement Input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Movement and Animation Logic
        if (Input.GetKey(KeyCode.W)) // Forward
        {
            animator.ResetTrigger("WalkBackwards");
            animator.ResetTrigger("Stay");
            animator.SetTrigger("WalkForward");
        }
        else if (Input.GetKey(KeyCode.S)) // Backward
        {
            animator.ResetTrigger("WalkForward");
            animator.ResetTrigger("Stay");
            animator.SetTrigger("WalkBackwards");
        }
        else // Idle
        {
            animator.ResetTrigger("WalkForward");
            animator.ResetTrigger("WalkBackwards");
            animator.SetTrigger("Stay");
        }

        // Movement Translation
        transform.Translate(Vector3.forward * Time.deltaTime * 2.0f * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * 2.0f * horizontalInput);

        // Rotation Logic
        if (horizontalInput < 0)
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Sprint Logic
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5.0f * forwardInput);
            animator.ResetTrigger("WalkForward");
            animator.ResetTrigger("WalkBackwards");
            animator.SetTrigger("Sprint");
        }

        // Sneak Logic
        if (Input.GetKey(KeyCode.C) && !isSneaking)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 6.0f * forwardInput);
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }

        // Crawl Logic
        if (Input.GetKeyDown(KeyCode.V))
        {
            isCrawling = !isCrawling;
            if (isCrawling)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 4.5f * forwardInput);
                transform.localScale = new Vector3(transform.localScale.x, 0.4f * originalScale, transform.localScale.z);
                //transform.position += crawlPosition;
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 10.0f * forwardInput);
                transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
            }
        }
    }
}
