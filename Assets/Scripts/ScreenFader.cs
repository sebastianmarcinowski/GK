using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // Assign the black image here
    public float fadeDuration = 1f;
    public float fadeOutDuration = 0.5f;
    public float fadeInDuration = 1f;

    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0, fadeInDuration)); // Fade to transparent
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1, fadeOutDuration)); // Fade to black
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}