using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemainingText;
    public PlayerMovement playerMovement;
    public Level level;

    // Start is called before the first frame update
    void Start()
    {
        GameVariables.TimeRemaining = Constants.INITIAL_LEVEL_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        GameVariables.TimeRemaining -= Time.deltaTime;
        UpdateTimeText();
    }

    public void UpdateTimeText()
    {
        int remaining = (int)GameVariables.TimeRemaining;
        if(GameVariables.TimeRemaining < 0)
        {
            return;
        }
        timeRemainingText.text = (0 <= remaining && remaining < 10) ?
            "0" + remaining.ToString() : remaining.ToString();
    }

    public void ResetLevelTime()
    {
        GameVariables.TimeRemaining = LevelTime();
        UpdateTimeText();
    }

    private int LevelTime()
    {
        if (GameVariables.CurrentLevel == 0)
        {
            return Constants.INITIAL_LEVEL_TIME;
        }
        if (GameVariables.CurrentLevel % Constants.BOSS_FREQUENCY == 0)
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
