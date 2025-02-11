using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshObstacleToogle : MonoBehaviour
{
    public NavMeshObstacle obstacle;
    public GameObject obstacleObject;
    public GameObject activationButton;
    private bool isActive = false;
    private bool playerInRange = false;

    private void Start()
    {
        obstacle.enabled = false;
        obstacleObject.SetActive(false);
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateObstacle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void ActivateObstacle()
    {
        if (!isActive)
        {
            isActive = true;
            obstacle.enabled = true;
            obstacleObject.SetActive(true);
            activationButton.SetActive(false);
            Debug.Log("Przeszkoda aktywna");
        }
    }
}
