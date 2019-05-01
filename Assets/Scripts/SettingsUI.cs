using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;

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
            OnPropertyChanged("myColor");
            OnPropertyChanged("myButton");
        }
    }
    public Color UIColor {
        get { return _UIColor; }
        set {
            if (_UIColor == value) { return; }
            _UIColor = value;
            OnPropertyChanged("UIColor");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
            c = new Color(0.8863f, 0.3804f, 0.3804f, 1.0f);
        }
        else if (s == "Orange")
        {
            c = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
        }
        else if (s == "Yellow")
        {
            c = new Color(0.898f, 0.8118f, 0.3255f, 1.0f);
        }
        else if (s == "Teal")
        {
            c = new Color(0.3882f, 0.8784f, 0.9778f, 1.0f);
        }
        else if (s == "Purple")
        {
            c = new Color(0.6745f, 0.3882f, 0.8784f, 1.0f);
        }
        else if (s == "Pink")
        {
            c = new Color(0.9372f, 0.2627f, 0.7921f, 1.0f);
        }
        else
        {
            c = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
        }
        return c;
    }

    private int colorIndex(string clrstr) {
        switch (clrstr)
        {
            case "Red":     return 0;
            case "Orange":  return 1;
            case "Yellow":  return 2;
            case "Teal":    return 4;
            case "Purple":  return 5;
            case "Pink":    return 6;
            default:        return 3;
        }
    }
}
