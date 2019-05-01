using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void loadMenu(){
      PlayerGameManager.IncrementTimesPlayed();
      SceneManager.LoadScene("MainMenu");
      SceneManager.UnloadSceneAsync("GameScene");

      // Scene nextScene = SceneManager.GetSceneByName("MainMenu");
      // if(nextScene.IsValid()) {
      //   Scene activeScene = SceneManager.GetActiveScene();
      //   SceneManager.SetActiveScene(nextScene);
      //   int bInd = activeScene.buildIndex;
      //   SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(bInd));
      // }

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
