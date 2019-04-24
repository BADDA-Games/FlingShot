using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System;

public class MainMenuUI : MonoBehaviour
{
    //private Button _playButton;
    public Button playButton;
    public Button menuButton;
    public Graphic slideMenu;
    public InputField seedInput;
    public Image shotBody;
    public Button exitRules;
    public Button exitAbout;
    Color myColor = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);



    // Start is called before the first frame update
    void Start()
    {
        myColor = PlayerGameManager.GetColor();
        //Debug.Log(PlayerGameManager.GetColorName());
        ColorBlock colors = playButton.colors;
        colors.normalColor = myColor;
        playButton.colors = colors;
        menuButton.colors = colors;
        exitAbout.colors = colors;
        exitRules.colors = colors;
        slideMenu.color = myColor;
        shotBody.color = myColor;

    }

    // Update is called once per frame
    void Update()
    {
        if(myColor != PlayerGameManager.GetColor()) {
            myColor = PlayerGameManager.GetColor();
            ColorBlock colors = playButton.colors;
            colors.normalColor = myColor;
            playButton.colors = colors;
            exitRules.colors = colors;
            exitAbout.colors = colors;
            menuButton.colors = colors;
            slideMenu.color = myColor;
        }
    }


    public async Task loadGamePlay() {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        //Debug.Log("load Game");
        return;
    }

    public async void OpenGame_Seed() {
        string input = seedInput.text;
        //Debug.Log("Seed: " + input);
        int i = -1;
        if(input=="") {
            //Debug.Log("Empty");
            System.Random rnd = new System.Random();
            int value = rnd.Next(1, 99999989);
            PlayerGameManager.SeedValue = value;
            //Debug.Log(PlayerGameManager.SeedValue);
        }
        else {
            bool isInt = int.TryParse(input, out i);
            if (isInt)
            {
                //Debug.Log("IS INT");
                PlayerGameManager.SeedValue = i;
                //Debug.Log(PlayerGameManager.SeedValue);
            }
            else
            {
                //Debug.Log("NOT INT");
                return;
            }
        }
        SceneManager.LoadScene("GameScene");
        SceneManager.UnloadSceneAsync("MainMenu");
    }


    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
        SceneManager.UnloadSceneAsync("MainMenu");
    }

}
