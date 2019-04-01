using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour, INotifyPropertyChanged
{
    //private DataClass Data;
    public event PropertyChangedEventHandler PropertyChanged;
    public Dropdown myDropdown;
    public Button myButton;
    private bool _needToLoad = true;
    private Color _UIColor = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
    private Color _myColor = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
    //Scene mainmenuScene = SceneManager.GetSceneByBuildIndex(0);
    public Color myColor {
        get { return _myColor; }
        set {
            if (_myColor == value) { return; }
            _myColor = value;
            //Data.TheColor = value;
            //Debug.Log("Hello");
            OnPropertyChanged("myColor");
            OnPropertyChanged("myButton");
        }
    }
    public Color UIColor {
        get { return _UIColor; }
        set {
            if (_UIColor == value) { return; }
            _UIColor = value;
            //Debug.Log("World");
            OnPropertyChanged("UIColor");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Data = new DataClass();
        //Data = ScriptableObject.CreateInstance
        Debug.Log("Starting Settings");
        Color c = PlayerGameManager.GetColor();
        if(c != null) {
            Debug.Log("PGM gC returned null");
            UIColor = c;
        }
        else {
            Debug.Log("PGM gC returned value");
            UIColor = myColor;
        }
        //UIColor = myColor;
        ColorBlock colors = myButton.colors;
        colors.normalColor = UIColor;
        myButton.colors = colors;
        /*if(!mainmenuScene.isLoaded) {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerGameManager.LoadScene && SceneManager.GetActiveScene().name=="Settings") {
            //Debug.Log("loading last scene");
            PlayerGameManager.LoadScene = false;
            //TODO: fix to make a true back function
            //SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            //SceneManager.LoadSceneAsync(PlayerGameManager.LastScene.buildIndex, LoadSceneMode.Additive);
        }
        if(myButton.colors.normalColor != PlayerGameManager.GetColor()) {
            ColorBlock colors = myButton.colors;
            //colors.normalColor = Color.green;
            colors.normalColor = myColor;
            myButton.colors = colors;
            OnPropertyChanged("myButton");
            //Debug.Log("FORCE UPDATE");
        }
    }

    public void DD_Select() {
        //Debug.Log("Selection");
        Text txt = myDropdown.captionText;
        string str = txt.text;
        myColor = PlayerGameManager.UpdateGetColor(str);
        //myColor = MyGetColor(str);
        //UIColor = myColor;
        //Data.TheColor = myColor;
    }

    public void BackButton() {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        Scene nextScene = PlayerGameManager.LastScene;
        Scene thisScene = SceneManager.GetActiveScene();
        int i = 0;
        /*while(!nextScene.IsValid()) {
            i++;
        }*/
        Debug.Log(i);
        if (nextScene.IsValid()) {
            Debug.Log("CURRENT: S: " + thisScene.name);
            Debug.Log("NEXT:    S: " + nextScene.name);
            PlayerGameManager.LastScene = thisScene;
            PlayerGameManager.LoadScene = true;
            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(thisScene.buildIndex);
        }
    }

    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            //Debug.Log("Changing " + propertyName);
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public Color MyGetColor(string s)
    {
        Color c;
        if (s == "Red")
        {
            Debug.Log("Red");
            //return new Color(226, 97, 97, 255);
            c = new Color(0.8863f, 0.3804f, 0.3804f, 1.0f);
        }
        else if (s == "Orange")
        {
            Debug.Log("Orange");
            //return new Color(237, 151, 81, 255);
            c = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
        }
        else if (s == "Yellow")
        {
            Debug.Log("Yellow");
            //return new Color(229, 207, 83, 255);
            c = new Color(0.898f, 0.8118f, 0.3255f, 1.0f);
        }
        else if (s == "Teal")
        {
            Debug.Log("Teal");
            //return new Color(99, 224, 220, 255);
            c = new Color(0.3882f, 0.8784f, 0.9778f, 1.0f);
        }
        else if (s == "Purple")
        {
            Debug.Log("Purple");
            //return new Color(172, 99, 224, 255);
            c = new Color(0.6745f, 0.3882f, 0.8784f, 1.0f);
        }
        else if (s == "Pink")
        {
            Debug.Log("Pink");
            //return new Color(239, 67, 202, 255);
            c = new Color(0.9372f, 0.2627f, 0.7921f, 1.0f);
        }
        else
        {
            Debug.Log("Green");
            //return new Color(105, 224, 99, 255);
            c = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
        }
        return c;
    }
}
