using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject PauseButton;

    void Start()
    {
        Image panel = PauseUI.GetComponent<Image>();
        Image pauseButton = PauseButton.GetComponent<Image>();
        panel.color = PlayerGameManager.GetColor();
        pauseButton.color = PlayerGameManager.GetColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameVariables.IsGameOver) //TODO
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
        if (GameVariables.IsPaused)
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
        switch (GameVariables.IsPaused)
        {
            case true:
                Time.timeScale = 1;
                UnPause();
                PauseUI.SetActive(false);
                PauseButton.SetActive(true);
                break;
            case false:
                Time.timeScale = 0;
                GameVariables.IsPaused = true;
                PauseUI.SetActive(true);
                PauseButton.SetActive(false);
                break;
        }
    }

    private void UnPause()
    {
        GameVariables.IgnoreNextFrame = true;
        GameVariables.IsPaused = false;
    }
}
