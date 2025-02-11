using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public StatueMessage statueMessage;

    private void Start()
    {
        uiText.text = "Twoja misja: Wydostañ siê st¹d";
        StartCoroutine(changeTextDelayDouble());
    }
    public void wrenchAlert()
    {   
        uiText.text = "Ani drgnie, muszê znale¿æ jakiœ klucz";
        StartCoroutine(changeTextDelay());
    }
    public void wrenchPickupAlert()
    {
        uiText.text = "Jest klucz, to powinno pomóc z t¹ krat¹";
        StartCoroutine(changeTextDelay());
    }
    public void wireGameAlert()
    {
        uiText.text = "Naprawione, idê sprawdziæ te drzwi";
        StartCoroutine(changeTextDelay());
    }
    public void statueGameEnd()
    {
        uiText.text = "Uda³o siê!";
        StartCoroutine(changeTextDelay());
    }
    public void mazeKeyAlert()
    {
        uiText.text = "O, to chyba klucz do tych zamkniêtych drzwi";
        StartCoroutine(changeTextDelay());
    }
    public void mazeDoorAlert()
    {
        uiText.text = "Zamkniête, potrzebujê klucza ¿eby tam wejœæ";
        StartCoroutine(changeTextDelay());
    }
    public void statueCrate()
    {
        uiText.text = "Nic tu nie ma szukam dalej";
        StartCoroutine(changeTextDelayStatues());
        
    }
    public void statueCorrectCrate()
    {
        uiText.text = "Znalaz³em jak¹œ kartkê, co tu jest napisane?";
        StartCoroutine(correctCrate());
        statueMessage.isMessageActive = true;
    }
    public void statuesEnter()
    {
        StartCoroutine(changeTextDelayStatues());
    }
    private IEnumerator changeTextDelay()
    {
        yield return new WaitForSeconds(4);
        uiText.text = "";
    }
    private IEnumerator changeTextDelayDouble()
    {
        yield return new WaitForSeconds(8);
        uiText.text = "";
    }
    private IEnumerator changeTextDelayStatues()
    {
        yield return new WaitForSeconds(4);
        uiText.text = "ZnajdŸ w skrzyniach podpowiedŸ dla zagadki";
    }
    private IEnumerator correctCrate()
    {
        yield return new WaitForSeconds(4);
        uiText.text = "Naciœnij [I] aby wyœwietliæ podpowiedŸ";
    }
}
