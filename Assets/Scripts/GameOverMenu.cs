using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void loadMenu(){
      Debug.Log("Returning to main menu");
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

    public void restartGame(){
      // Debug.Log("Restarting Game");
      SceneManager.LoadScene("GameScene");
    }

}
