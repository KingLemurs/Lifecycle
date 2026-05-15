using UnityEngine;

public class PauseUI : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void Resume()
    {
        gameManager.paused = false;
    }

    public void Options()
    {

    }

    public void MainMenu()
    {
        Application.Quit();
    }
}
