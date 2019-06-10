using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

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

}
