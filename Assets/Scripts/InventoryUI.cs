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
        uiText.text = "Twoja misja: Wydosta� si� st�d";
        StartCoroutine(changeTextDelayDouble());
    }
    public void wrenchAlert()
    {   
        uiText.text = "Ani drgnie, musz� znale�� jaki� klucz";
        StartCoroutine(changeTextDelay());
    }
    public void wrenchPickupAlert()
    {
        uiText.text = "Jest klucz, to powinno pom�c z t� krat�";
        StartCoroutine(changeTextDelay());
    }
    public void wireGameAlert()
    {
        uiText.text = "Naprawione, id� sprawdzi� te drzwi";
        StartCoroutine(changeTextDelay());
    }
    public void statueGameEnd()
    {
        uiText.text = "Uda�o si�!";
        StartCoroutine(changeTextDelay());
    }
    public void mazeKeyAlert()
    {
        uiText.text = "O, to chyba klucz do tych zamkni�tych drzwi";
        StartCoroutine(changeTextDelay());
    }
    public void mazeDoorAlert()
    {
        uiText.text = "Zamkni�te, potrzebuj� klucza �eby tam wej��";
        StartCoroutine(changeTextDelay());
    }
    public void statueCrate()
    {
        uiText.text = "Nic tu nie ma szukam dalej";
        StartCoroutine(changeTextDelayStatues());
        
    }
    public void statueCorrectCrate()
    {
        uiText.text = "Znalaz�em jak�� kartk�, co tu jest napisane?";
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
        uiText.text = "Znajd� w skrzyniach podpowied� dla zagadki";
    }
    private IEnumerator correctCrate()
    {
        yield return new WaitForSeconds(4);
        uiText.text = "Naci�nij [I] aby wy�wietli� podpowied�";
    }
}
