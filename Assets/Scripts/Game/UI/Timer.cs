using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemainingText;
    public float TimeRemaining { get; set; }
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        TimeRemaining = Constants.INITIAL_LEVEL_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
    }

    public void UpdateTimeText()
    {
        int remaining = (int)TimeRemaining;
        if(TimeRemaining < 0)
        {
            return;
        }
        if(0 <= remaining && remaining < 10)
        {
            timeRemainingText.text = "0" + remaining.ToString();
        }
        else
        {
            timeRemainingText.text = remaining.ToString();
        }
    }

    public void ResetLevelTime()
    {
        TimeRemaining = LevelTime();
        UpdateTimeText();
    }

    private int LevelTime()
    {
        if (playerMovement.currentLevel == 0)
        {
            return Constants.INITIAL_LEVEL_TIME;
        }
        if (playerMovement.currentLevel % Constants.BOSS_FREQUENCY == 0)
        {
            return Constants.BOSS_LEVEL_TIME;
        }
        switch (PlayerGameManager.GetDifficulty())
        {
            case "Easy":
                return Constants.EASY_TIME;
            case "Medium":
                return Constants.MEDIUM_TIME;
            case "Hard":
                return Constants.HARD_TIME;
            default:
                Debug.Log("Cannot determine appropriate level timer!");
                return Constants.MEDIUM_TIME;
        }
    }
}
