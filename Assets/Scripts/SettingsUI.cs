using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

public class SettingsUI : MonoBehaviour, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public Dropdown myDropdown;
    public Button myButton;
    public Button resetButton;
    public Text myHighScore;
    public Text myLastScore;
    public Text myTimesPlayed;
    public Text myTitle;
    private Color _UIColor = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
    private Color _myColor = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
    private string colorName = "Green";

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
        //Debug.Log("Starting Settings");
        Color c = PlayerGameManager.GetColor();
        if(c != null) {
            UIColor = c;
            colorName = PlayerGameManager.GetColorName();
            myDropdown.value = colorIndex(colorName);
        }
        else {
            //Debug.Log("PGM gC returned null");
            UIColor = myColor;
        }
        ColorBlock colors = myButton.colors;
        colors.normalColor = UIColor;
        myButton.colors = colors;
        resetButton.colors = colors;
        myHighScore.text = "" + PlayerGameManager.GetHighScore();
        myLastScore.text = "" + PlayerGameManager.GetLastScore();
        myTimesPlayed.text = "" + PlayerGameManager.GetTimesPlayed();
        OnPropertyChanged("myDropdown");

    }

    // Update is called once per frame
    void Update()
    {
        if(myButton.colors.normalColor != PlayerGameManager.GetColor()) {
            myColor = PlayerGameManager.GetColor();
            colorName = PlayerGameManager.GetColorName();
            myDropdown.value = colorIndex(colorName);
            ColorBlock colors = myButton.colors;
            colors.normalColor = myColor;
            myButton.colors = colors;
            resetButton.colors = colors;
            OnPropertyChanged("myButton");
        }
        if(myTimesPlayed.text != ""+PlayerGameManager.GetTimesPlayed()) {
            myHighScore.text = "" + PlayerGameManager.GetHighScore();
            myLastScore.text = "" + PlayerGameManager.GetLastScore();
            myTimesPlayed.text = "" + PlayerGameManager.GetTimesPlayed();
            colorName = PlayerGameManager.GetColorName();
            myColor = PlayerGameManager.GetColor();
            myDropdown.value = colorIndex(colorName);
            ColorBlock colors = myButton.colors;
            colors.normalColor = myColor;
            myButton.colors = colors;
            resetButton.colors = colors;
            OnPropertyChanged("myButton");
        }
    }

    public void DD_Select() {
        //Debug.Log("SELECTION");
        Text txt = myDropdown.captionText;
        string str = txt.text;
        myColor = PlayerGameManager.UpdateGetColor(str);
    }

    public void BackButton() {
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("Settings");
    }

    public async Task loadMainMenu() {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        return;
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
            //Debug.Log("Red");
            //return new Color(226, 97, 97, 255);
            c = new Color(0.8863f, 0.3804f, 0.3804f, 1.0f);
        }
        else if (s == "Orange")
        {
            //Debug.Log("Orange");
            //return new Color(237, 151, 81, 255);
            c = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
        }
        else if (s == "Yellow")
        {
            //Debug.Log("Yellow");
            //return new Color(229, 207, 83, 255);
            c = new Color(0.898f, 0.8118f, 0.3255f, 1.0f);
        }
        else if (s == "Teal")
        {
            //Debug.Log("Teal");
            //return new Color(99, 224, 220, 255);
            c = new Color(0.3882f, 0.8784f, 0.9778f, 1.0f);
        }
        else if (s == "Purple")
        {
            //Debug.Log("Purple");
            //return new Color(172, 99, 224, 255);
            c = new Color(0.6745f, 0.3882f, 0.8784f, 1.0f);
        }
        else if (s == "Pink")
        {
            //Debug.Log("Pink");
            //return new Color(239, 67, 202, 255);
            c = new Color(0.9372f, 0.2627f, 0.7921f, 1.0f);
        }
        else
        {
            //Debug.Log("Green");
            //return new Color(105, 224, 99, 255);
            c = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
        }
        return c;
    }

    private int colorIndex(string clrstr) {
        if(clrstr == "Red") {
            return 0;
        }
        else if(clrstr == "Orange") {
            return 1;
        }
        else if(clrstr == "Yellow") {
            return 2;
        }
        else if(clrstr == "Teal") {
            return 4;
        }
        else if(clrstr == "Purple") {
            return 5;
        }
        else if(clrstr == "Pink") {
            return 6;
        }
        else {
            return 3;
        }
    }
}
