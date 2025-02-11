using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueMinigame : MonoBehaviour
{
    [System.Serializable]
    public class Statue
    {
        public GameObject statueObject; // Obiekt pos¹gu
        public string backwardTrigger = "Backward"; // Nazwa triggera dla animacji cofaniowej
        public string forwardTrigger = "Forward"; // Nazwa triggera dla animacji resetuj¹cej
        public Animator animator; // Animator pos¹gu
    }

    public List<Statue> statues; // Lista pos¹gów
    public KeyCode interactionKey = KeyCode.E; // Klawisz interakcji
    public float interactionRange = 2.5f; // Maksymalna odleg³oœæ interakcji
    public GameObject player; // Transform gracza

    private List<int> correctOrder = new List<int>(); // Poprawna kolejnoœæ aktywacji
    private int currentStep = 0; // Aktualny krok

    public GameObject UIText;
    private InventoryUI inventoryUI;
    public EndingScreen endingScreen;
    void Start()
    {
        inventoryUI = UIText.GetComponent<InventoryUI>();
        GenerateOrder();
    }

    void Update()
    {
        CheckInteraction();
    }

    // Generuje poprawn¹ kolejnoœæ pos¹gów (np. losow¹ kolejnoœæ)
    private void GenerateOrder()
    {
        for (int i = 0; i < statues.Count; i++)
        {
            correctOrder.Add(i);
        }
    }
    private void CheckInteraction()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            foreach (var statue in statues)
            {
                if (IsPlayerInRange(statue.statueObject))
                {
                    HandleStatueInteraction(statues.IndexOf(statue));
                    return;
                }
            }
        }
    }
     
    private bool IsPlayerInRange(GameObject statue)
    {
        float distance = Vector3.Distance(player.transform.position, statue.transform.position);
        return distance <= interactionRange;
    }

    private void HandleStatueInteraction(int index)
    {
        if (index == correctOrder[currentStep])
        {
            if (statues[index].animator == null)
            {
                return;
            }
            if (statues[index].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                !statues[index].animator.IsInTransition(0))
            {
                statues[index].animator.SetTrigger(statues[index].backwardTrigger);
            }
            currentStep++;
            if (currentStep >= correctOrder.Count)
            {
                StartCoroutine(showEnding());
            }
        }
        else
        {
            ResetStatues();
        }
    }

    private IEnumerator showEnding()
    {
        yield return new WaitForSeconds(4);
        endingScreen.ShowEndingScreen();
    }

    private void ResetStatues()
    {
        currentStep = 0;
        foreach (var statue in statues)
        {
            Debug.Log($"Resetowanie pos¹gu {statue.statueObject.name}");
            statue.animator.SetTrigger(statue.forwardTrigger);
        }
    }

}
