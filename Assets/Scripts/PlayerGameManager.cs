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
            value = value % 99999989;
            _seedValue = value;
        }
    }

    private static Player _player;
    public static Player player
    {
        get
        {
            //Debug.Log("getting player");
            if (_player == null)
            {
                //Debug.Log("PGM: player == null");
                StorageHandler storageHandler = new StorageHandler();
                _player = (Player)storageHandler.LoadData("player"); //Here
                if(_player == null) {
                    //Debug.Log("CREATING NEW PLAYER;");
                    _player = new Player();
                    _player.PlayerColor = "Green";
                    _player.PlayerDifficulty = "Medium";
                    _player.PlayerHighScore = 0;
                    _player.PlayerLastScore = 0;
                    _player.PlayerNumberTimesPlayed = 0;
                }
            }
            return _player;
        }
        set { _player = value; }
    }

    private static void Save() {
        StorageHandler storage = new StorageHandler();
        storage.SaveData(player, "player"); //error
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
        Save(); // error
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
        player.PlayerHighScore = hs;
        Save();
    }
    public static int UpdateGetHighScore(int hs) {
        player.PlayerHighScore = hs;
        Save();
        return player.PlayerHighScore;
    }

    public static void UpdateLastScore(int ls) {
        player.PlayerLastScore = ls;
        Save();
    }
    public static int UpdateGetLastScore(int ls)
    {
        player.PlayerLastScore = ls;
        Save();
        return player.PlayerLastScore;
    }

    public static void UpdateTimesPlayed(int tp) {
        player.PlayerNumberTimesPlayed = tp;
        Save();
    }
    /*
    public static void UpdateSound(bool s) {
        player.PlayerSound = s;
        Save();
    }

    public static bool UpdateGetSound(bool s) {
        player.PlayerSound = s;
        Save();
        return player.PlayerSound;
    }
    */
    public static void IncrementTimesPlayed() {
        int tp = player.PlayerNumberTimesPlayed;
        tp = tp + 1;
        player.PlayerNumberTimesPlayed = tp;
        Save();
    }
    public static int IncrementGetTimesPlayed()
    {
        int tp = player.PlayerNumberTimesPlayed;
        tp = tp + 1;
        player.PlayerNumberTimesPlayed = tp;
        Save();
        return player.PlayerNumberTimesPlayed;
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
        return player.PlayerHighScore;
    }

    public static int GetLastScore() {
        return player.PlayerLastScore;
    }

    public static int GetTimesPlayed() {
        return player.PlayerNumberTimesPlayed;
    }

    public static string GetColorName() {
        return player.PlayerColor;
    }
    /*
    public static bool GetSound() {
        return player.PlayerSound;
    }
    */
}
