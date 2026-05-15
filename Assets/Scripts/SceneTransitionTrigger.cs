using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Required References")]
    public TimeSkipTrigger timeSkipTrigger;
    public Image fadeImage;

    [Header("Scene Settings")]
    public string nextSceneName = "NextSceneName";

    [Header("Fade Settings")]
    public float fadeDuration = 2f;

    private bool isTransitioning = false;
    private void Start()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) return;

        if (!other.CompareTag("Player")) return;

        if (timeSkipTrigger == null || !timeSkipTrigger.timeSkipFinished)
        {
            return;
        }

        StartCoroutine(WhiteFadeAndLoadScene());
    }

    private IEnumerator WhiteFadeAndLoadScene()
    {
        isTransitioning = true;

        float timer = 0f;

        Color color = Color.white;
        color.a = 0f;
        fadeImage.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = timer / fadeDuration;

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        SceneManager.LoadScene(nextSceneName);
    }
}