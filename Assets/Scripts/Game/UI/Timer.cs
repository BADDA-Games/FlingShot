using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timeRemainingText;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameVariables.GameType)
        {
            case GameType.Standard:
                GameVariables.TimeRemaining = Constants.INITIAL_LEVEL_TIME;
                break;
            case GameType.Puzzle:
                GameVariables.TimeRemaining = GameVariables.CurrentDifficulty;
                break;
            default:
                Debug.Log("Invalid GameType detected!");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameVariables.GameType)
        {
            case GameType.Standard:
                GameVariables.TimeRemaining -= Time.deltaTime;
                UpdateTimeText();
                break;
            case GameType.Puzzle:
                break;
            default:
                GameVariables.TimeRemaining -= Time.deltaTime;
                UpdateTimeText();
                break;
        }
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
        switch (GameVariables.GameType)
        {
            case GameType.Standard:
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
                    case Difficulty.Easy:
                        return Constants.EASY_TIME;
                    case Difficulty.Medium:
                        return Constants.MEDIUM_TIME;
                    case Difficulty.Hard:
                        return Constants.HARD_TIME;
                    default:
                        Debug.Log("Cannot determine appropriate level timer!");
                        return Constants.MEDIUM_TIME;
                }
            case GameType.Puzzle:
                if(GameVariables.CurrentLevel % Constants.BOSS_FREQUENCY == 0 && GameVariables.CurrentLevel > 0)
                {
                    return Constants.THUNK_PUZZLE_MOVES;
                }
                return GameVariables.CurrentDifficulty;
            default:
                Debug.Log("Cannot produce a valid Level Time for this game mode!");
                return -1;

        }

    }
}
