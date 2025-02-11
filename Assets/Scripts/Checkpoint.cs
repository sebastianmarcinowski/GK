using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class CheckpointSystem : MonoBehaviour
{
    private Vector3 lastCheckpointPosition;
    public GameObject player;
    public List<GameObject> enemies; // List to store multiple enemies
    private List<Vector3> initialEnemyPositions;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f; // Duration for fade in/out

    void Start()
    {
        // Create a canvas
        canvas = new GameObject("CheckpointCanvas").AddComponent<Canvas>();
        // Initially disable the canvas
        canvas.gameObject.SetActive(false);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Add a CanvasGroup for fade animations
        canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f; // Initially invisible

        // Create a black background
        GameObject background = new GameObject("Background");
        RectTransform bgTransform = background.AddComponent<RectTransform>();
        bgTransform.SetParent(canvas.transform);
        bgTransform.anchorMin = Vector2.zero; // Bottom-left
        bgTransform.anchorMax = Vector2.one; // Top-right
        bgTransform.offsetMin = Vector2.zero; // Reset offsets
        bgTransform.offsetMax = Vector2.zero;

        Image bgImage = background.AddComponent<Image>();
        bgImage.color = Color.black; // Black background

        // Store the initial positions of the player and all enemies
        lastCheckpointPosition = player.transform.position;
        initialEnemyPositions = new List<Vector3>();

        foreach (GameObject enemy in enemies)
        {
            initialEnemyPositions.Add(enemy.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player enters a checkpoint collider, update the checkpoint position
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = other.transform.position;
            Debug.Log("Checkpoint updated to: " + lastCheckpointPosition);
        }
    }

    public void HandlePlayerCaught()
    {
        StartCoroutine(SmoothRespawn());
    }

    private IEnumerator SmoothRespawn()
    {
        // Step 1: Fade out (to black)
        yield return FadeOut();

        // Step 2: Move the player and reset enemies
        player.transform.position = lastCheckpointPosition;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].transform.position = initialEnemyPositions[i];
            }
        }

        Debug.Log("Player respawned and enemies reset.");

        // Step 3: Fade in (to transparent)
        yield return FadeIn();
    }

    private IEnumerator FadeOut()
    {
        canvas.gameObject.SetActive(true);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        canvas.gameObject.SetActive(false);
    }
}