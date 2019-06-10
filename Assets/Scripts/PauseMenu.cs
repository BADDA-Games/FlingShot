using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject PauseUI;

    void Start()
    {
        Image panel = PauseUI.GetComponent<Image>();
        panel.color = PlayerGameManager.GetColor();
    }

    //TODO TEMPORARY - Remove before production!
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
                break;
            case false:
                Time.timeScale = 0;
                isPaused = true;
                PauseUI.SetActive(true);
                break;
        }
    }
}
