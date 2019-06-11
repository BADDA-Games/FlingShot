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

    void Start()
    {

    }

    void Update()
    {
        if(player.health >= 0)
        {
            HeartUI.sprite = HeartSprites[player.health];
        }
    }
}
