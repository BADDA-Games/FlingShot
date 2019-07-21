using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public Sprite[] HeartSprites;
    public Image HeartUI;

    void Start()
    {

    }

    void Update()
    {
        switch (GameVariables.GameType)
        {
            case GameType.Endless:
                HeartUI.enabled = false;
                break;
            default:
                if (GameVariables.Health >= 0)
                {
                    HeartUI.sprite = HeartSprites[GameVariables.Health];
                }
                break;
        }
    }
}
