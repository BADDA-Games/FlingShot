using UnityEngine;
using System;

[Serializable]
public class Player
{
    public string PlayerColor;
    public int PlayerHighScore;
    public int PlayerLastScore;
    public int PlayerNumberTimesPlayed;
}

[Serializable]
public class PlayerUD_1_1 {
    public int PHS_Easy;
    public int PHS_Medium;
    public int PHS_Hard;
    public int PLS_Easy;
    public int PLS_Medium;
    public int PLS_Hard;
    public int PTP_Easy;
    public int PTP_Medium;
    public int PTP_Hard;
    //
    public string PlayerDifficulty;
    public string PlayerColor;
    public int PlayerHighScore;
    public int PlayerLastScore;
    public int PlayerNumberTimesPlayed;
}
