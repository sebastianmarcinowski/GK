using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel UI ekwipunku
    public Text inventoryText; // Tekst, w kt�rym b�d� wypisane klucze

    private PlayerInventory playerInventory;
    private bool isInventoryVisible = false;

    void Start()
    {
        // Ukryj panel ekwipunku na pocz�tku
        inventoryPanel.SetActive(false);

        // Znajd� komponent PlayerInventory
        playerInventory = FindObjectOfType<PlayerInventory>();

        
    }

    void Update()
    {
        // Sprawd�, czy gracz nacisn�� przycisk "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        // Prze��cz widoczno�� ekwipunku
        isInventoryVisible = !isInventoryVisible;
        inventoryPanel.SetActive(isInventoryVisible);

        if (isInventoryVisible)
        {
            UpdateInventoryUI();
        }
    }

    void UpdateInventoryUI()
    {
        // Wyczy�� poprzedni� zawarto�� tekstu ekwipunku
        inventoryText.text = "Ekwipunek:\n";

        // Pobierz list� kluczy i wy�wietl je
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
