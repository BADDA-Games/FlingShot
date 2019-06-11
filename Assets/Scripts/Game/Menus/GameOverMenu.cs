using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;
    public Text seedText;
    public GameObject GameOverUI;
    public GameObject PauseButton;

    public void LoadMainMenu(){
      PlayerGameManager.IncrementTimesPlayed();
      SceneManager.LoadScene("MainMenu");
      SceneManager.UnloadSceneAsync("GameScene");
    }

    public void RestartGame(){
      PlayerGameManager.IncrementTimesPlayed();
      SceneManager.LoadScene("GameScene");
    }

    public void NewSeed()
    {
        PlayerGameManager.IncrementTimesPlayed();
        System.Random rnd = new System.Random();
        PlayerGameManager.SeedValue = rnd.Next(1, 99999989);
        SceneManager.LoadScene("GameScene");
    }

    public void TriggerGameOver()
    {
        int score;
        switch (PlayerGameManager.GetDifficulty())
        {
            case "Easy":
                score = Convert.ToInt32(GameVariables.TotalTimeTaken * GameVariables.CurrentLevel * Constants.EASY_SCORE_MODIFIER);
                break;
            case "Hard":
                score = Convert.ToInt32(GameVariables.TotalTimeTaken * GameVariables.CurrentLevel * Constants.HARD_SCORE_MODIFIER);
                break;
            default:
                score = Convert.ToInt32(GameVariables.TotalTimeTaken * GameVariables.CurrentLevel * Constants.MEDIUM_SCORE_MODIFIER);
                break;
        }
        scoreText.text = "Score: " + score.ToString();
        seedText.text = "Seed: " + GameVariables.Seed.ToString();

        PlayerGameManager.UpdateLastScore(score);

        if (PlayerGameManager.GetHighScore() < score)
        {
            PlayerGameManager.UpdateHighScore(score);
        }

        if (GameOverUI != null)
        {
            GameOverUI.SetActive(true);
        }
        PauseButton.SetActive(false);
        GameVariables.IsGameOver = true;
    }

}
