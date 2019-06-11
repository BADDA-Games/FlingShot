using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject PauseUI;
    public GameObject PauseButton;
    public PlayerMovement playerMovement;

    void Start()
    {
        Image panel = PauseUI.GetComponent<Image>();
        Image pauseButton = PauseButton.GetComponent<Image>();
        panel.color = PlayerGameManager.GetColor();
        pauseButton.color = PlayerGameManager.GetColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerMovement.isGameOver)
        {
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                Pause();
            }
        }
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("GameScene");
    }

    public void Resume()
    {
        if (isPaused)
        {
            Pause();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void Pause()
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 1;
                isPaused = false;
                PauseUI.SetActive(false);
                PauseButton.SetActive(true);
                break;
            case false:
                Time.timeScale = 0;
                isPaused = true;
                PauseUI.SetActive(true);
                PauseButton.SetActive(false);
                break;
        }
    }
}
