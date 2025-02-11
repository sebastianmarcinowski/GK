using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatueMessage : MonoBehaviour
{
    // Message to display
    // Message to display
    private string message = "Czterech stra¿ników strzeg³o sekretów w milczeniu. Pierwszy, ciê¿ki jak kamieñ, trwa³ nieruchomo. Drugi, zimny jak marmur, niós³ echo szlachetnej pieœni. Trzeci, lœni¹cy miedzi¹, zna³ blask przysz³ych dni. Czwarty, skorodowany, szepta³ sekrety przesz³oœci.\n\nPierwszy nigdy nie sta³ na krañcu. Drugi zawsze blisko trzeciego. Trzeci unika³ czwartego, choæ ich losy by³y splecione. Czwarty, kruchy, trwa³ najd³u¿ej, kryj¹c tajemnice czasu.\n\nStra¿nicy czekali na tego, kto odgadnie ich zagadkê.";

    // Reference to the UI elements
    private GameObject messagePanel;
    private Text messageText;
    public bool isMessageActive = false;
    void Start()
    {
        // Create a panel to cover the screen
        messagePanel = new GameObject("MessagePanel");
        messagePanel.AddComponent<CanvasRenderer>();

        Canvas canvas = messagePanel.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = messagePanel.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        messagePanel.SetActive(false);
        messagePanel.AddComponent<GraphicRaycaster>();

        // Create a background image for the panel
        GameObject background = new GameObject("Background");
        background.transform.SetParent(messagePanel.transform, false);

        Image backgroundImage = background.AddComponent<Image>();
        backgroundImage.color = new Color(0.9f, 0.8f, 0.6f, 1f); // Paper-like color

        RectTransform backgroundRect = background.GetComponent<RectTransform>();
        backgroundRect.anchorMin = new Vector2(0, 0);
        backgroundRect.anchorMax = new Vector2(1, 1);
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;

        // Create the text element
        GameObject textObject = new GameObject("MessageText");
        textObject.transform.SetParent(messagePanel.transform, false);

        messageText = textObject.AddComponent<Text>();
        messageText.text = message;
        messageText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        messageText.fontSize = 24;
        messageText.color = Color.black;
        messageText.alignment = TextAnchor.MiddleCenter;

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.1f, 0.1f);
        textRect.anchorMax = new Vector2(0.9f, 0.9f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

    }

    void Update()
    {
        // Show or hide the message when the "I" key is pressed
        if (Input.GetKeyDown(KeyCode.I) && isMessageActive == true)
        {
            messagePanel.SetActive(!messagePanel.activeSelf);
        }
    }
}
