using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int doorID; // Unikalne ID drzwi
    public bool isOpen = false; // Status drzwi (otwarte/zamkniête)

    private Animator doorAnimation;

    void Start()
    {
        GameObject doorParent = transform.root.gameObject;
        doorAnimation = doorParent.GetComponent<Animator>();
    }

    public void TryOpenDoor(int keyID)
    {
        if (keyID == doorID) // Sprawdza, czy gracz posiada odpowiedni klucz
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("Potrzebny odpowiedni klucz, aby otworzyæ te drzwi.");
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        doorAnimation.SetTrigger("open"); // Wywo³uje animacjê otwierania drzwi
        Debug.Log("Drzwi zosta³y otwarte!");
    }
}
