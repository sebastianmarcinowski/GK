using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField] private float stunDuration = 3.0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                StartCoroutine(StunPlayer(playerMovement));
            }
        }
    }

    private IEnumerator StunPlayer(PlayerMovement player)
    {
        player.enabled = false;
        Debug.Log("Player is stunned");
        yield return new WaitForSeconds(stunDuration);
        Debug.Log("Player is no longer stunned");
        player.enabled = true;
        Destroy(gameObject);
    }
}
