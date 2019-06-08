using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private bool isPaused;
    public GameObject PauseUI;

    void Start()
    {
        PauseUI = GameObject.Find("PauseMenu");
        Debug.Log(PauseUI == null);
    }

    //TODO TEMPORARY - Remove before production!
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Pause();
        }
    }

    public void Quit()
    {

    }

    public void Resume()
    {

    }

    public void Pause()
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 1;
                isPaused = false;
                if (PauseUI != null)
                {
                    PauseUI.SetActive(false);
                }
                break;
            case false:
                Time.timeScale = 0;
                isPaused = true;
                if (PauseUI != null)
                {
                    PauseUI.SetActive(true);
                }
                break;
        }
    }
}
