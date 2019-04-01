using UnityEngine;
using System;

[Serializable]
public class Player 
{
    /*private const string color = "Color";
    public string PlayerColor {
        get { return PlayerPrefs.GetString(color); }
        set { PlayerPrefs.SetString(color, value); }
    }

    private const string highScore = "High Score";
    public int PlayerHighScore {
        get { return PlayerPrefs.GetInt(highScore); }
        set { PlayerPrefs.SetInt(highScore, value); }
    }

    private const string lastScore = "Last Score";
    public int PlayerLastScore {
        get { return PlayerPrefs.GetInt(lastScore); }
        set { PlayerPrefs.SetInt(lastScore, value); }
    }

    public const string timesPlayed = "Times Played";
    public int PlayerNumberTimesPlayed {
        get { return PlayerPrefs.GetInt(timesPlayed); }
        set { PlayerPrefs.SetInt(timesPlayed, value); }
    }*/

    public string PlayerColor;
    public int PlayerHighScore;
    public int PlayerLastScore;
    public int PlayerNumberTimesPlayed;
}
