using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public int keyID;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.AddKey(keyID);
            Destroy(gameObject);
        }
    }
}
