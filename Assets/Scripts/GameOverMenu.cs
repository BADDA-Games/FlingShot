using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void loadMenu(){
      Debug.Log("Returning to main menu");

    }

    public void restartGame(){
      // Debug.Log("Restarting Game");
      SceneManager.LoadScene("GameScene");
    }

}
