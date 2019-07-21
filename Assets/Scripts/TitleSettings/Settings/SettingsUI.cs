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
    public Dropdown DiffDrop;
    public Button myButton;
    public Button resetButton;
    public Text myHighScore;
    public Text myLastScore;
    public Text myTimesPlayed;
    public Text myTitle;
    public Text myDifficulty;
    public Text HighScoreHeader;
    public Text LastScoreHeader;
    public Text TimesPlayedHeader;
    private Color _UIColor = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
    private Color _myColor = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
    private string colorName = "Green";
    private Difficulty difficulty = Difficulty.Easy;

    public Color myColor {
        get { return _myColor; }
        set {
            if (_myColor == value) { return; }
            _myColor = value;
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
            myDropdown.value = ColorIndex(colorName);
        }
        else {
            //Debug.Log("PGM gC returned null");
            UIColor = myColor;
        }
        Difficulty d = PlayerGameManager.GetDifficulty();
        if(d != null)
        {
            difficulty = PlayerGameManager.GetDifficulty();
            DiffDrop.value = DifficultyIndex(difficulty);
        }
        else
        {
            DiffDrop.value = DifficultyIndex(Difficulty.Easy);
        }
        ColorBlock colors = myButton.colors;
        colors.normalColor = UIColor;
        myButton.colors = colors;
        resetButton.colors = colors;
        myHighScore.text = "" + PlayerGameManager.GetHighScore();
        myLastScore.text = "" + PlayerGameManager.GetLastScore();
        myTimesPlayed.text = "" + PlayerGameManager.GetTimesPlayed();
        OnPropertyChanged("myDropdown");
        OnPropertyChanged("DiffDrop");

    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerGameManager.GetDifficulty())
        {
            case Difficulty.Endless:
                HighScoreHeader.text = "Current Level";
                LastScoreHeader.text = "Seed";
                TimesPlayedHeader.text = "Time Taken";
                break;
            default:
                HighScoreHeader.text = "High Score";
                LastScoreHeader.text = "Last Score";
                TimesPlayedHeader.text = "Times Played";
                break;
        }
        if(myButton.colors.normalColor != PlayerGameManager.GetColor()) {
            myColor = PlayerGameManager.GetColor();
            colorName = PlayerGameManager.GetColorName();
            difficulty = PlayerGameManager.GetDifficulty();
            myDropdown.value = ColorIndex(colorName);
            DiffDrop.value = DifficultyIndex(difficulty);
            ColorBlock colors = myButton.colors;
            colors.normalColor = myColor;
            myButton.colors = colors;
            resetButton.colors = colors;
            OnPropertyChanged("myButton");
        }
        if(myTimesPlayed.text != ""+PlayerGameManager.GetTimesPlayed() || myHighScore.text != ""+PlayerGameManager.GetHighScore() || myLastScore.text != ""+PlayerGameManager.GetLastScore()) {
            myHighScore.text = "" + PlayerGameManager.GetHighScore();
            myLastScore.text = "" + PlayerGameManager.GetLastScore();
            myTimesPlayed.text = "" + PlayerGameManager.GetTimesPlayed();
            colorName = PlayerGameManager.GetColorName();
            myColor = PlayerGameManager.GetColor();
            difficulty = PlayerGameManager.GetDifficulty();
            myDropdown.value = ColorIndex(colorName);
            DiffDrop.value = DifficultyIndex(difficulty);
            ColorBlock colors = myButton.colors;
            colors.normalColor = myColor;
            myButton.colors = colors;
            resetButton.colors = colors;
            //DiffDrop.value = DifficultyIndex(myDifficulty.text);
            OnPropertyChanged("myButton");
        }
    }

    public void DD_Select() {
        Text txt = myDropdown.captionText;
        string str = txt.text;
        myColor = PlayerGameManager.UpdateGetColor(str);
    }

    public void Diff_Select(){
        Text txt = DiffDrop.captionText;
        string str = txt.text;
        Difficulty diff = StringDifficulty(str);
        difficulty = PlayerGameManager.UpdateGetDifficulty(diff);
        myDifficulty.text = DifficultyString(difficulty);
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
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Color MyGetColor(string s)
    {
        Color c;
        switch (s)
        {
            case "Red":
                c = new Color(0.8863f, 0.3804f, 0.3804f, 1.0f);
                break;
            case "Orange":
                c = new Color(0.9294f, 0.5921f, 0.3176f, 1.0f);
                break;
            case "Yellow":
                c = new Color(0.898f, 0.8118f, 0.3255f, 1.0f);
                break;
            case "Teal":
                c = new Color(0.3882f, 0.8784f, 0.9778f, 1.0f);
                break;
            case "Purple":
                c = new Color(0.6745f, 0.3882f, 0.8784f, 1.0f);
                break;
            case "Pink":
                c = new Color(0.9372f, 0.2627f, 0.7921f, 1.0f);
                break;
            default:
                c = new Color(0.4117f, 0.8784f, 0.3882f, 1.0f);
                break;
        }
        return c;
    }

    private int ColorIndex(string clrstr) {
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
    private int DifficultyIndex(Difficulty diff) {
        switch (diff)
        {
            case Difficulty.Easy: 
                return 0;
            case Difficulty.Medium:
                return 1;
            case Difficulty.Hard:
                return 2;
            case Difficulty.Puzzle:
                return 3;
            case Difficulty.Endless:
                return 4;
            default:
                return 0;
        }
    }

    private string DifficultyString(Difficulty diff)
    {
        switch (diff)
        {
            case Difficulty.Easy:
                return "Easy";
            case Difficulty.Medium:
                return "Medium";
            case Difficulty.Hard:
                return "Hard";
            case Difficulty.Puzzle:
                return "Puzzle";
            case Difficulty.Endless:
                return "Endless";
            default:
                return "Easy";
        }
    }

    private Difficulty StringDifficulty(string diffString)
    {
        switch (diffString)
        {
            case "Easy":
                return Difficulty.Easy;
            case "Medium":
                return Difficulty.Medium;
            case "Hard":
                return Difficulty.Hard;
            case "Puzzle":
                return Difficulty.Puzzle;
            case "Endless":
                return Difficulty.Endless;
            default:
                return Difficulty.Easy;
        }
    }
}
