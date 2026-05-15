using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSkipTrigger : MonoBehaviour
{
    [Header("UI References")]
    public GameObject clockPanel;
    public RectTransform clockHand;
    public TMP_Text timeText;
    public Image fadeImage;

    [Header("Effect Settings")]
    public float effectDuration = 3f;
    public float handSpinSpeed = 1200f;
    public float fadeSpeed = 2f;

    private bool hasTriggered = false;

    private void Start()
    {
        // Hide clock at start
        if (clockPanel != null)
        {
            clockPanel.SetActive(false);
            clockHand.gameObject.SetActive(false);
        }

        // Make fade screen invisible at start
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(TimeSkipEffect());
            Debug.Log("Time skip triggered!");
        }
    }

    private IEnumerator TimeSkipEffect()
    {
        // Show the clock only when triggered
        clockPanel.SetActive(true);
        clockHand.gameObject.SetActive(true);

        // Fade screen slightly darker
        yield return StartCoroutine(FadeTo(0.75f));

        float timer = 0f;

        while (timer < effectDuration)
        {
            Debug.Log($"Time skip effect running... Timer: {timer:F2}s");
            timer += Time.deltaTime;

            // Spin the clock hand
            if (clockHand != null)
            {
                clockHand.Rotate(0f, 0f, -handSpinSpeed * Time.deltaTime);
            }

            // Update fake time text
            if (timeText != null)
            {
                float progress = timer / effectDuration;
                int hour = Mathf.FloorToInt(Mathf.Lerp(12, 24, progress));

                if (hour >= 24)
                {
                    hour -= 24;
                }

                timeText.text = hour.ToString("00") + ":00";
            }

            yield return null;
        }

        // Fade back to normal
        yield return StartCoroutine(FadeTo(0f));

        // Hide clock after effect finishes
        clockPanel.SetActive(false);
        clockHand.gameObject.SetActive(false);
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        if (fadeImage == null) yield break;

        Color color = fadeImage.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = color;
            yield return null;
        }
    }
}