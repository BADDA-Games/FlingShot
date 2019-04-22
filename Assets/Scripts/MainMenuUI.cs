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
    public Button settingButton;
    public Graphic slideMenu;
    public InputField seedInput;
    public Image shotBody;
    public Button exitInstr;
    public Button exitRules;
    public Button exitAbout;
    //public Graphic menuPanel;
    //private DataClass Data;
    Color myColor = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
    //UnityEngine.AsyncOperation asyncSettings;
   //Scene settingScene = SceneManager.GetSceneByBuildIndex(1);
    //private ColorBlock theColors;


    // Start is called before the first frame update
    void Start()
    {
        //playButton.colors.normalColor = MenuFunctions.CurrentColor();
        //Data = new DataClass();
        //myColor = new Color(105, 224, 99, 255);
        //playButton.image.color = myColor;
        /*ColorBlock cb = playButton.colors;
        cb.normalColor = myColor;
        playButton.colors = cb;*/
        //Debug.Log("Starting MainMenu");
        myColor = PlayerGameManager.GetColor();
        //Debug.Log(PlayerGameManager.GetColorName());
        ColorBlock colors = playButton.colors;
        colors.normalColor = myColor;
        playButton.colors = colors;
        settingButton.colors = colors;
        exitAbout.colors = colors;
        exitInstr.colors = colors;
        exitRules.colors = colors;
        slideMenu.color = myColor;
        shotBody.color = myColor;
        
        //loadScenes();
        /*if(!settingScene.isLoaded) {
            SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
        }*/
        //SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
        //PlayerGameManager.LastScene = SceneManager.GetActiveScene();
        //async.allowSceneActivation = false;

        //menuPanel.color = myColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerGameManager.LoadScene && SceneManager.GetActiveScene().name=="MainMenu") {
            //Debug.Log("loading settings");
            PlayerGameManager.LoadScene = false;
            SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
            //SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        }
        if(myColor != PlayerGameManager.GetColor()) {
            myColor = PlayerGameManager.GetColor();
            ColorBlock colors = playButton.colors;
            colors.normalColor = myColor;
            playButton.colors = colors;
            exitRules.colors = colors;
            exitInstr.colors = colors;
            exitAbout.colors = colors;
            settingButton.colors = colors;
            slideMenu.color = myColor;
        }
        //playButton.image.color = MenuFunctions.CurrentColor();
        /*
        if (myColor != Data.TheColor) {
            Debug.Log("REAL UPDATE : " + myColor.ToString() + " & " + Data.TheColor.ToString());
            myColor = Data.TheColor;
            ColorBlock cb = playButton.colors;
            cb.normalColor = myColor;
            playButton.colors = cb;
            OnPropertyChanged("playButton");
            */
            /*if (OnVariableChange != null)
            {
                OnVariableChange(myColor);
            }*/
            //playButton.image.color = myColor;
            /*ColorBlock cb = playButton.colors;
            cb.normalColor = myColor;
            playButton.colors = cb;*/
            //playButton.GetComponent<Button>().colors = cb;
            //playButton.colors.normalColor = myColor;
        /*
        }
        */
    }

  public async void OpenGame() {
        //Debug.Log("OpenGame");
        //SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        //TODO: add in loading circle
        await loadGamePlay();
        Scene nextScene = SceneManager.GetSceneByName("GameScene");
        if(nextScene.IsValid() && nextScene.isLoaded) {
            Scene activeScene = SceneManager.GetActiveScene();
            //Debug.Log("CURRENT: MM: " + activeScene.name);
            //Debug.Log("NEXT:    MM: " + nextScene.name);
            PlayerGameManager.LastScene = activeScene;
            PlayerGameManager.LoadScene = true;
            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        }
        else {
            //Debug.Log("Game not valid. Hmmm");
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        }
    }

    public async Task loadGamePlay() {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
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
        }
        else {
            bool isInt = int.TryParse(input, out i);
            if (isInt)
            {
                //Debug.Log("IS INT");
                PlayerGameManager.SeedValue = i;
            }
            else
            {
                //Debug.Log("NOT INT");
                return;
            }
        }
        await loadGamePlay();
        Scene nextScene = SceneManager.GetSceneByName("GameScene");
        if (nextScene.IsValid() && nextScene.isLoaded)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            PlayerGameManager.LastScene = activeScene;
            PlayerGameManager.LoadScene = true;
            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        }
        else
        {
            //Debug.Log("Game not valid. Hmmm");
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        }
    }


    public void OpenSettings()
    {
        //UnityEngine.AsyncOperation async;
        //Debug.Log("Open Settings");
        //openingSettings();
        //loadScenes();
        //asyncSettings.allowSceneActivation = true;
        //SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
        Scene nextScene = SceneManager.GetSceneByName("Settings");
        if(nextScene.IsValid()) {
            Scene activeScene = SceneManager.GetActiveScene();
            //Debug.Log("CURRENT: MM: " + activeScene.name);
            //Debug.Log("NEXT:    MM: " + nextScene.name);
            PlayerGameManager.LastScene = activeScene;
            PlayerGameManager.LoadScene = true;
            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
            //async.allowSceneActivation = true;
        }
        else {
            //Debug.Log("Settings not valid. Hmmm");
        }
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Settings"));
    }

    /*IEnumerator openingSettings() {
        yield return SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
    }*/


}
