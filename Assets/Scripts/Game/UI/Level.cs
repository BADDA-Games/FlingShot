using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text currentLevelText;
    public Timer timer;

    void Awake()
    {
        switch (PlayerGameManager.GetDifficulty())
        {
            case Difficulty.Puzzle:
                GameVariables.Reset(GameType.Puzzle);
                break;
            case Difficulty.Endless:
                GameVariables.Reset(GameType.Endless);
                if(GameVariables.IsUsingRandomSeed)
                {
                    GameVariables.CurrentLevel = PlayerGameManager.GetCurrentLevelEndless();
                    GameVariables.TimeRemaining = PlayerGameManager.GetCurrentLevelTimePlayedEndless();
                    GameVariables.TotalTimeTaken = PlayerGameManager.GetTotalTimePlayedEndless();
                    GameVariables.Seed = PlayerGameManager.SeedValue;
                    PlayerGameManager.SetInitialSeedEndless(PlayerGameManager.SeedValue);
                }
                break;
            default:
                GameVariables.Reset(GameType.Standard);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLevelText()
    {
        currentLevelText.text = GameVariables.CurrentLevel < 10 ?
            "0" + GameVariables.CurrentLevel.ToString() :
            GameVariables.CurrentLevel.ToString();
        timer.ResetLevelTime();
    }

    public void AdvanceLevel()
    {
        GameVariables.CurrentLevel++;
        UpdateLevelText();
    }
}
