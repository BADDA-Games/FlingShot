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
                value *= -1;
            }
            value = value % 100000000;
            _seedValue = value;
        }
    }

    private static PlayerUD_1_2 Migrate_1_0_To_1_2(Player p)
    {
        PlayerUD_1_2 _p = new PlayerUD_1_2
        {
            PHS_Hard = p.PlayerHighScore,
            PLS_Hard = p.PlayerLastScore,
            PTP_Hard = p.PlayerNumberTimesPlayed,
            PlayerColor = p.PlayerColor,

            PHS_Easy = 0,
            PLS_Easy = 0,
            PTP_Easy = 0,
            PHS_Medium = 0,
            PLS_Medium = 0,
            PTP_Medium = 0,
            PHS_Puzzle = 0,
            PLS_Puzzle = 0,
            PTP_Puzzle = 0,
            PlayerDifficulty = Difficulty.Easy
        };
        return _p;
    }

    private static PlayerUD_1_2 Migrate_1_1_To_1_2(PlayerUD_1_1 p)
    {
        PlayerUD_1_2 _p = new PlayerUD_1_2
        {
            PHS_Easy = p.PHS_Easy,
            PHS_Medium = p.PHS_Medium,
            PHS_Hard = p.PHS_Hard,
            PLS_Easy = p.PLS_Easy,
            PLS_Medium = p.PLS_Medium,
            PLS_Hard = p.PLS_Hard,
            PTP_Easy = p.PTP_Easy,
            PTP_Medium = p.PTP_Medium,
            PTP_Hard = p.PTP_Hard,
            PlayerColor = p.PlayerColor
        };

        switch (p.PlayerDifficulty)
        {
            case "Easy":
                _p.PlayerDifficulty = Difficulty.Easy;
                break;
            case "Medium":
                _p.PlayerDifficulty = Difficulty.Medium;
                break;
            case "Hard":
                _p.PlayerDifficulty = Difficulty.Hard;
                break;
            default:
                _p.PlayerDifficulty = Difficulty.Easy;
                break;
        }

        _p.PHS_Puzzle = 0;
        _p.PLS_Puzzle = 0;
        _p.PTP_Puzzle = 0;
        return _p;
    }

    private static PlayerUD_1_3 Migrate_1_2_To_1_3(PlayerUD_1_2 p)
    {
        PlayerUD_1_3 _p = new PlayerUD_1_3
        {
            PHS_Easy = p.PHS_Easy,
            PHS_Medium = p.PHS_Medium,
            PHS_Hard = p.PHS_Hard,
            PHS_Puzzle = p.PHS_Puzzle,
            PLS_Easy = p.PLS_Easy,
            PLS_Medium = p.PLS_Medium,
            PLS_Hard = p.PLS_Hard,
            PLS_Puzzle = p.PLS_Puzzle,
            PTP_Easy = p.PTP_Easy,
            PTP_Medium = p.PTP_Medium,
            PTP_Hard = p.PTP_Hard,
            PTP_Puzzle = p.PTP_Puzzle,
            PlayerDifficulty = p.PlayerDifficulty,
            PlayerColor = p.PlayerColor,
            END_LevelNumber = 0,
            END_TotalTime = 0,
            END_CurrentLevelTime = 0,
            END_CurrentSeed = -1,
            END_NextSeed = -1,
            END_InitialSeed = -1
        };
        return _p;
    }

    private static PlayerUD_1_3 _player;
    public static PlayerUD_1_3 player
    {
        get
        {
            if(_player != null)
            {
                return _player;
            }
            StorageHandler storageHandler = new StorageHandler();
            _player = (PlayerUD_1_3)storageHandler.LoadData("playerUDonethree");
            if (_player != null)
            {
                return _player;
            }

            PlayerUD_1_2 _player12 = (PlayerUD_1_2)storageHandler.LoadData("playedUDonetwo");
            if(_player12 != null)
            {
                return Migrate_1_2_To_1_3(_player12);
            }

            PlayerUD_1_1 _player11 = (PlayerUD_1_1)storageHandler.LoadData("playerUDoneone");
            if (_player11 != null)
            {
                _player12 =  Migrate_1_1_To_1_2(_player11);
                return Migrate_1_2_To_1_3(_player12);
            }

            Player _player10 = (Player)storageHandler.LoadData("player");
            if (_player10 != null)
            {
                _player12 = Migrate_1_0_To_1_2(_player10);
                return Migrate_1_2_To_1_3(_player12);
            }
            // Set all fields to default values
            _player = new PlayerUD_1_3
            {
                PHS_Easy = 0,
                PLS_Easy = 0,
                PTP_Easy = 0,
                PHS_Medium = 0,
                PLS_Medium = 0,
                PTP_Medium = 0,
                PHS_Hard = 0,
                PLS_Hard = 0,
                PTP_Hard = 0,
                PHS_Puzzle = 0,
                PLS_Puzzle = 0,
                PTP_Puzzle = 0,
                PlayerDifficulty = Difficulty.Easy,
                PlayerColor = "Green",
                //
                END_LevelNumber = 0,
                END_TotalTime = 0,
                END_CurrentLevelTime = 0,
                END_CurrentSeed = -1,
                END_NextSeed = -1,
                END_InitialSeed = -1
            };
            return _player;
        }
        set { _player = value; }
    }

    private static void Save() {
        StorageHandler storage = new StorageHandler();
        storage.SaveData(player, "playerUDonethree");
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

    public static void UpdateDifficulty(Difficulty pd) {
        player.PlayerDifficulty = pd;
        Save();
    }

    public static Difficulty UpdateGetDifficulty(Difficulty pd) {
        player.PlayerDifficulty = pd;
        Save();
        return player.PlayerDifficulty;
    }

    public static void UpdateHighScore(int hs) {
        Difficulty pdiff = player.PlayerDifficulty;
        SetPHS(pdiff, hs);
        Save();
    }

    public static int UpdateGetHighScore(int hs) {
        Difficulty pdiff = player.PlayerDifficulty;
        SetPHS(pdiff, hs);
        Save();
        return GetPHS(pdiff);
    }

    public static void UpdateLastScore(int ls) {
        Difficulty pdiff = player.PlayerDifficulty;
        SetPLS(pdiff, ls);
        Save();
    }

    public static int UpdateGetLastScore(int ls)
    {
        Difficulty pdiff = player.PlayerDifficulty;
        SetPHS(pdiff, ls);
        Save();
        return GetPHS(pdiff);
    }

    public static void UpdateTimesPlayed(int tp) {
        Difficulty pdiff = player.PlayerDifficulty;
        SetPTP(pdiff, tp);
        Save();
    }

    public static void IncrementTimesPlayed() {
        Difficulty pdiff = player.PlayerDifficulty;
        int tp = GetPTP(pdiff);
        UpdateTimesPlayed(tp + 1);
        Save();
    }

    public static int IncrementGetTimesPlayed()
    {
        Difficulty pdiff = player.PlayerDifficulty;
        int tp = GetPTP(pdiff);
        UpdateTimesPlayed(tp + 1);
        Save();
        return tp + 1;
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

    public static Difficulty GetDifficulty() {
        return player.PlayerDifficulty;
    }

    public static int GetHighScore() {
        Difficulty pdiff = player.PlayerDifficulty;
        return GetPHS(pdiff);
    }

    public static int GetLastScore() {
        Difficulty pdiff = player.PlayerDifficulty;
        return GetPLS(pdiff);
    }

    public static int GetTimesPlayed() {
        Difficulty pdiff = player.PlayerDifficulty;
        return GetPTP(pdiff);
    }

    public static string GetColorName() {
        return player.PlayerColor;
    }
    /*
    public static bool GetSound() {
        return player.PlayerSound;
    }
    */

    public static int GetPHS(Difficulty difficulty) {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return player.PHS_Easy;
            case Difficulty.Medium:
                return player.PHS_Medium;
            case Difficulty.Hard:
                return player.PHS_Hard;
            case Difficulty.Puzzle:
                return player.PHS_Puzzle;
            case Difficulty.Endless:
                return player.END_LevelNumber;
            default:
                return -1;
        }
    }

    public static void SetPHS(Difficulty difficulty, int hs) {
        switch (difficulty)
        {
            case Difficulty.Easy:
                player.PHS_Easy = hs;
                break;
            case Difficulty.Medium:
                player.PHS_Medium = hs;
                break;
            case Difficulty.Hard:
                player.PHS_Hard = hs;
                break;
            case Difficulty.Puzzle:
                player.PHS_Puzzle = hs;
                break;
            case Difficulty.Endless:
                player.END_LevelNumber = hs;
                break;
        }
    }

    public static int GetPLS(Difficulty difficulty) {
        switch (difficulty)
        {
            case Difficulty.Easy: 
                return player.PLS_Easy;
            case Difficulty.Medium:
                return player.PLS_Medium;
            case Difficulty.Hard:
                return player.PLS_Hard;
            case Difficulty.Puzzle:
                return player.PLS_Puzzle;
            case Difficulty.Endless:
                return player.END_InitialSeed;
            default:
                return -1;
        }
    }

    public static void SetPLS(Difficulty difficulty, int ls)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                player.PLS_Easy = ls;
                break;
            case Difficulty.Medium:
                player.PLS_Medium = ls;
                break;
            case Difficulty.Hard:
                player.PLS_Hard = ls;
                break;
            case Difficulty.Puzzle:
                player.PLS_Puzzle = ls;
                break;
            case Difficulty.Endless:
                player.END_InitialSeed = ls;
                break;
        }
    }

    public static int GetPTP(Difficulty difficulty) {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return player.PTP_Easy;
            case Difficulty.Medium:
                return player.PTP_Medium;
            case Difficulty.Hard:
                return player.PTP_Hard;
            case Difficulty.Puzzle:
                return player.PTP_Puzzle;
            case Difficulty.Endless:
                return player.END_TotalTime;
            default:
                return -1;
        }
    }

    public static void SetPTP(Difficulty difficulty, int tp)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                player.PTP_Easy = tp;
                break;
            case Difficulty.Medium:
                player.PTP_Medium = tp;
                break;
            case Difficulty.Hard:
                player.PTP_Hard = tp;
                break;
            case Difficulty.Puzzle:
                player.PTP_Puzzle = tp;
                break;
            case Difficulty.Endless:
                player.END_TotalTime = tp;
                break;
        }
    }

    public static int GetInitialSeedEndless()
    {
        return player.END_InitialSeed;
    }
    public static void SetInitialSeedEndless(int seed)
    {
        player.END_InitialSeed = seed;
        Save();
    }

    public static int GetCurrentSeedEndless()
    {
        return player.END_CurrentSeed;
    }
    public static void SetCurrentSeedEndless(int seed)
    {
        player.END_CurrentSeed = seed;
        Save();
    }
    public static int GetNextSeedEndless()
    {
        return player.END_NextSeed;
    }
    public static void SetNextSeedEndless(int seed)
    {
        player.END_NextSeed = seed;
        Save();
    }

    public static int GetCurrentLevelEndless()
    {
        return player.END_LevelNumber;
    }
    public static void IncrementCurrentLevelEndless()
    {
        SetCurrentLevelEndless(player.END_LevelNumber + 1);
    }
    public static void SetCurrentLevelEndless(int level)
    {
        player.END_LevelNumber = level;
        Save();
    }

    public static int GetTotalTimePlayedEndless()
    {
        return player.END_TotalTime;
    }
    public static void SetTotalTimePlayedEndless(int time)
    {
        player.END_TotalTime = time;
        Save();
    }
    public static void AddTotalTimePlayedEndless(int time)
    {
        player.END_TotalTime += time;
        Save();
    }

    public static int GetCurrentLevelTimePlayedEndless()
    {
        return player.END_CurrentLevelTime;
    }
    public static void SetCurrentLevelTimePlayedEndless(int time)
    {
        player.END_CurrentLevelTime = time;
        Save();
    }
}