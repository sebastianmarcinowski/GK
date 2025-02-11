using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndingScreen : MonoBehaviour
{
    private string endingMessage = "Dziêkujê serdecznie za uwagê. \n Projekt powsta³ w ramach przedmiotu: Gry Komputerowe. \nAutor:Sebastian Marcinowski."; // Customizable message
    public Font messageFont; // Optional: Assign a font in the Inspector
    public float fadeDuration = 1.0f; // Duration of the fade-in animation

    private Canvas canvas;
    private CanvasGroup canvasGroup; // For controlling transparency
    private Text messageText;

    void Start()
    {
        // Create a canvas
        canvas = new GameObject("EndingCanvas").AddComponent<Canvas>();
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

        // Create the message text
        GameObject message = new GameObject("EndingMessage");
        RectTransform messageTransform = message.AddComponent<RectTransform>();
        messageTransform.SetParent(canvas.transform);
        messageTransform.anchorMin = new Vector2(0.1f, 0.4f); // Position the message
        messageTransform.anchorMax = new Vector2(0.9f, 0.6f);
        messageTransform.offsetMin = Vector2.zero;
        messageTransform.offsetMax = Vector2.zero;

        messageText = message.AddComponent<Text>();
        messageText.text = endingMessage; // Set the message text
        messageText.color = Color.white; // White text
        messageText.alignment = TextAnchor.MiddleCenter; // Center align
        messageText.fontSize = 40;

        // Use assigned font or default font
        if (messageFont != null)
        {
            messageText.font = messageFont;
        }
        else
        {
            messageText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }
    }

    // Show the ending screen with a fade-in effect
    public void ShowEndingScreen()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    // Hide the ending screen
    public void HideEndingScreen()
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    // Fade-in animation
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // Gradually increase alpha
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure it's fully visible
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
