using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;
    public PlayerMovement player;
    public GameObject PauseUI;

    private bool isPaused;

    void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(player.health >= 0)
        {
            HeartUI.sprite = HeartSprites[player.health];
        }
    }

    void Pause()
    {
        switch (isPaused)
        {
            case true:
                Time.timeScale = 1;
                isPaused = false;
                break;
            case false:
                Time.timeScale = 0;
                isPaused = true;
                break;
        }
    }
}
