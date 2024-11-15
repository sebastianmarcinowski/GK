using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel UI ekwipunku
    public Text inventoryText; // Tekst, w którym bêd¹ wypisane klucze

    private PlayerInventory playerInventory;
    private bool isInventoryVisible = false;

    void Start()
    {
        // Ukryj panel ekwipunku na pocz¹tku
        inventoryPanel.SetActive(false);

        // ZnajdŸ komponent PlayerInventory
        playerInventory = FindObjectOfType<PlayerInventory>();

        
    }

    void Update()
    {
        // SprawdŸ, czy gracz nacisn¹³ przycisk "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        // Prze³¹cz widocznoœæ ekwipunku
        isInventoryVisible = !isInventoryVisible;
        inventoryPanel.SetActive(isInventoryVisible);

        if (isInventoryVisible)
        {
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        // Wyczyœæ poprzedni¹ zawartoœæ tekstu ekwipunku
        inventoryText.text = "Ekwipunek:\n";

        // Pobierz listê kluczy i wyœwietl je
        List<int> keys = playerInventory.GetKeys();
        if (keys.Count > 0)
        {
            foreach (int key in keys)
            {
                inventoryText.text += "Klucz o ID: " + key + "\n";
            }
        }
        else
        {
            inventoryText.text += "Brak kluczy.";
        }
    }
}
