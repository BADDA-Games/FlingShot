using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

//Who ever wrote this thread
//https://forum.unity.com/threads/simple-local-data-storage.468936/
//deserves a shrine with worshippers
//and my first born child named after them

public class StorageHandler {
    public void SaveData(object objectToSave, string fileName) {
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        BinaryFormatter Formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(FullFilePath, FileMode.Create); // error
        //FileStream fileStream = File.Open(FullFilePath, FileMode.OpenOrCreate);
        Formatter.Serialize(fileStream, objectToSave);
        fileStream.Close();
    }

    public object LoadData(string fileName) {
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        if (File.Exists(FullFilePath))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Open);
            if(fileStream.Length == 0) {
                fileStream.Close();
                return null;
            }
            else {
                object obj = Formatter.Deserialize(fileStream); //Here
                fileStream.Close();
                return obj;
            }
        }
        else { return null; }
    }
}

public static class PlayerGameManager
{
    private static int _seedValue = -1;
    public static int SeedValue {
        get{
            return _seedValue;
        }
        set {
            if (value < 0) {
                value = value * -1;
            }
            value = value % 100000000;
            _seedValue = value;
        }
    }

    private static PlayerUD_1_1 _player;
    public static PlayerUD_1_1 player
    {
        get
        {
            //Debug.Log("getting player");
            if (_player == null)
            {
                /*
                StorageHandler storageHandler = new StorageHandler();
                _player = (Player)storageHandler.LoadData("player"); 
                if(_player == null) {
                    _player = new Player();
                    _player.PlayerColor = "Green";
                    _player.PlayerHighScore = 0;
                    _player.PlayerLastScore = 0;
                    _player.PlayerNumberTimesPlayed = 0;
                }
                */
                StorageHandler storageHandler = new StorageHandler();
                _player = (PlayerUD_1_1)storageHandler.LoadData("playerUDoneone");
                if(_player == null) {
                    _player = new PlayerUD_1_1();
                    Player temp = (Player)storageHandler.LoadData("player");
                    if(temp != null) {
                        _player.PlayerColor = temp.PlayerColor;
                        _player.PHS_Hard = temp.PlayerHighScore;
                        _player.PLS_Hard = temp.PlayerLastScore;
                        _player.PTP_Hard = temp.PlayerNumberTimesPlayed;
                        _player.PlayerDifficulty = "Hard";
                    }
                    else {
                        _player.PlayerColor = "Green";
                        _player.PHS_Hard = 0;
                        _player.PLS_Hard = 0;
                        _player.PTP_Hard = 0;
                        _player.PlayerDifficulty = "Hard";
                    }
                    _player.PHS_Easy = 0;
                    _player.PHS_Medium = 0;
                    _player.PLS_Easy = 0;
                    _player.PLS_Medium = 0;
                    _player.PTP_Easy = 0;
                    _player.PTP_Medium = 0;
                }
            }
            return _player;
        }
        set { _player = value; }
    }

    private static void Save() {
        StorageHandler storage = new StorageHandler();
        storage.SaveData(player, "playerUDoneone");
    }

    /*private static Scene _lastScene = SceneManager.GetSceneByName("MainMenu");
    public static Scene LastScene
    {
        get { return _lastScene; }
        set { _lastScene = value; }
    }
    private static bool _loadScene = true;
    public static bool LoadScene
    {
        get { return _loadScene; }
        set { _loadScene = value; }

    }*/

    public static void UpdateColor(string c) {
        player.PlayerColor = c;
        Save();
    }
    public static Color UpdateGetColor(string c) {
        player.PlayerColor = c;
        Save(); 
        Color temp = GetColor();
        return temp;
    }

    public static void UpdateDifficulty(string c) {
        player.PlayerDifficulty = c;
        Save();
    }

    public static string UpdateGetDifficulty(string c) {
        player.PlayerDifficulty = c;
        Save();
        return player.PlayerDifficulty;
    }

    public static void UpdateHighScore(int hs) {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy") {
            player.PHS_Easy = hs;
        }
        else if(pdiff == "Medium") {
            player.PHS_Medium = hs;
        }
        else {
            player.PHS_Hard = hs;
        }
        Save();
    }

    public static int UpdateGetHighScore(int hs) {
        string pdiff = player.PlayerDifficulty;
        int returner;
        if (pdiff == "Easy")
        {
            player.PHS_Easy = hs;
            returner = player.PHS_Easy;
        }
        else if (pdiff == "Medium")
        {
            player.PHS_Medium = hs;
            returner = player.PHS_Medium;
        }
        else
        {
            player.PHS_Hard = hs;
            returner = player.PHS_Hard;
        }
        Save();
        return returner;
    }

    public static void UpdateLastScore(int ls) {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy")
        {
            player.PLS_Easy = ls;
        }
        else if (pdiff == "Medium")
        {
            player.PLS_Medium = ls;
        }
        else
        {
            player.PLS_Hard = ls;
        }
        Save();
    }

    public static int UpdateGetLastScore(int ls)
    {
        string pdiff = player.PlayerDifficulty;
        int returner;
        if (pdiff == "Easy")
        {
            player.PLS_Easy = ls;
            returner = player.PLS_Easy;
        }
        else if (pdiff == "Medium")
        {
            player.PLS_Medium = ls;
            returner = player.PLS_Medium;
        }
        else
        {
            player.PLS_Hard = ls;
            returner = player.PLS_Hard;
        }
        Save();
        return returner;
    }

    public static void UpdateTimesPlayed(int tp) {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy")
        {
            player.PTP_Easy = tp;
        }
        else if (pdiff == "Medium")
        {
            player.PTP_Medium = tp;
        }
        else
        {
            player.PTP_Hard = tp;
        }
        Save();
    }

    public static void IncrementTimesPlayed() {
        string pdiff = player.PlayerDifficulty;
        int tp;
        if (pdiff == "Easy")
        {
            tp = player.PTP_Easy;
            tp = tp + 1;
            player.PTP_Easy = tp;
        }
        else if (pdiff == "Medium")
        {
            tp = player.PTP_Medium;
            tp = tp + 1;
            player.PTP_Medium = tp;
        }
        else
        {
            tp = player.PTP_Hard;
            tp = tp + 1;
            player.PTP_Hard = tp;
        }
        Save();
    }

    public static int IncrementGetTimesPlayed()
    {
        string pdiff = player.PlayerDifficulty;
        int tp;
        if (pdiff == "Easy")
        {
            tp = player.PTP_Easy;
            tp = tp + 1;
            player.PTP_Easy = tp;
        }
        else if (pdiff == "Medium")
        {
            tp = player.PTP_Medium;
            tp = tp + 1;
            player.PTP_Medium = tp;
        }
        else
        {
            tp = player.PTP_Hard;
            tp = tp + 1;
            player.PTP_Hard = tp;
        }
        Save();
        return tp;
    }

    public static Color GetColor() {
        string s = player.PlayerColor;
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

    public static string GetDifficulty() {
        return player.PlayerDifficulty;
    }

    public static int GetHighScore() {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy")
        {
            return player.PHS_Easy;
        }
        else if (pdiff == "Medium")
        {
            return player.PHS_Medium;
        }
        else
        {
            return player.PHS_Hard;
        }
    }

    public static int GetLastScore() {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy")
        {
            return player.PLS_Easy;
        }
        else if (pdiff == "Medium")
        {
            return player.PLS_Medium;
        }
        else
        {
            return player.PLS_Hard;
        }
    }

    public static int GetTimesPlayed() {
        string pdiff = player.PlayerDifficulty;
        if (pdiff == "Easy")
        {
            return player.PTP_Easy;
        }
        else if (pdiff == "Medium")
        {
            return player.PTP_Medium;
        }
        else
        {
            return player.PTP_Hard;
        }
    }

    public static string GetColorName() {
        return player.PlayerColor;
    }
    /*
    public static bool GetSound() {
        return player.PlayerSound;
    }
    */

    public static int GetPHS(string difficulty) {
        if (difficulty == "Easy")
        {
            return player.PHS_Easy;
        }
        else if (difficulty == "Medium")
        {
            return player.PHS_Medium;
        }
        else
        {
            return player.PHS_Hard;
        }
    }

    public static void SetPHS(string difficulty, int hs) {
        if (difficulty == "Easy")
        {
            player.PHS_Easy = hs;
        }
        else if (difficulty == "Medium")
        {
            player.PHS_Medium = hs;
        }
        else
        {
            player.PHS_Hard = hs;
        }
    }

    public static int GetLHS(string difficulty) {
        if (difficulty == "Easy")
        {
            return player.PLS_Easy;
        }
        else if (difficulty == "Medium")
        {
            return player.PLS_Medium;
        }
        else
        {
            return player.PLS_Hard;
        }
    }

    public static void SetPLS(string difficulty, int ls)
    {
        if (difficulty == "Easy")
        {
            player.PLS_Easy = ls;
        }
        else if (difficulty == "Medium")
        {
            player.PLS_Medium = ls;
        }
        else
        {
            player.PLS_Hard = ls;
        }
    }

    public static int GetPTP(string difficulty) {
        if (difficulty == "Easy")
        {
            return player.PTP_Easy;
        }
        else if (difficulty == "Medium")
        {
            return player.PTP_Medium;
        }
        else
        {
            return player.PTP_Hard;
        }
    }

    public static void SetPTP(string difficulty, int tp)
    {
        if (difficulty == "Easy")
        {
            player.PTP_Easy = tp;
        }
        else if (difficulty == "Medium")
        {
            player.PTP_Medium = tp;
        }
        else
        {
            player.PTP_Hard = tp;
        }
    }
}
