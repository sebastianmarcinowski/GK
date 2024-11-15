using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rg;

    //Zmienne do skoku
    int doubleJumpCheck = 2;
    private int jumpCounter = 0;
    public LayerMask floorLayer;
    private BoxCollider boxCollider;
    RaycastHit hit;
    float groundRadius = 0.05f;
    private bool onGround;
    private bool wasOnGround;

    //Zmienne do WASD
    private float horizontalInput;
    private float forwardInput;
    public float rotationSpeed = 5f;

    //Zmienne do czolgania i kucania
    private bool isSneaking;
    private bool isCrawling;
    private Vector3 crawlPosition, startPosition;
    private float originalScale;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        floorLayer = LayerMask.GetMask("FloorLayer");
        boxCollider = GetComponent<BoxCollider>();

        startPosition = transform.position;
        originalScale = transform.localScale.y;
        crawlPosition = new Vector3(0.0f, -3.0f,  0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        onGround = Physics.Raycast(transform.position, -Vector3.up, out hit,boxCollider.bounds.extents.y + groundRadius, floorLayer);
        //Debug.Log(onGround);
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < doubleJumpCheck && isCrawling == false && isSneaking == false)
        {
            jumpCounter++;
            rg.velocity = new Vector3(rg.velocity.x, 0f, rg.velocity.z);
            rg.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
        if (onGround && !wasOnGround)
        {
            jumpCounter = 0;
        }
        wasOnGround = onGround; // aktualizuje stan do nastêpnej ramki
        //WASD movement
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * 10.0f * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * 10.0f * horizontalInput);
        if (horizontalInput < 0) // obrót w lewo przy "A"
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0) // obrót w prawo przy "D"
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }


        //Sprint
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 16.0f * forwardInput);
        }
        //Sneak
        if (Input.GetKey(KeyCode.C) && isSneaking == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 6.0f * forwardInput);
            isSneaking = true;
        }
        else
        {
            isSneaking = false;
        }
        //Czo³ganie i zmiana wielkoœci
        if (Input.GetKeyDown(KeyCode.V))
        {
            isCrawling = !isCrawling;
            if (isCrawling)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 4.5f * forwardInput);
                transform.localScale = new Vector3(transform.localScale.x, 0.4f * originalScale, transform.localScale.z);
                transform.position += crawlPosition;
                isCrawling = true;
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 10.0f * forwardInput);
                transform.localScale = new Vector3(transform.localScale.x, originalScale, transform.localScale.z);
                isCrawling = false;
            }
        }
    }
}
