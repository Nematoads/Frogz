using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool running = false;
    public GameObject gameOverPanel;
    public GameObject ReplayInGameButton;


    private void Start()
    {
        EventBroker.gameOver += ShowGameOverScreen;
    }
    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOverScreen()
    {
        ReplayInGameButton.SetActive(false);
        gameOverPanel.SetActive(true);
        Debug.Log("SetActive");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        EventBroker.gameOver -= ShowGameOverScreen;
    }
}
