using System;
public static class GameVariables
{
    public static float TimeRemaining;
    public static int CurrentLevel;
    public static int CurrentDifficulty;
    public static int Seed;
    public static bool IsGameOver;
    public static bool IsPaused;
    public static bool IgnoreNextFrame;
    public static int TotalTimeTaken;
    public static int Health;
    public static GameType GameType;
    public static bool UsingKey;

    public static void Reset()
    {
        TimeRemaining = Constants.INITIAL_LEVEL_TIME;
        CurrentLevel = 0;
        CurrentDifficulty = Constants.STARTER_LEVEL_DIFFICULTY;
        Seed = -1;
        IsGameOver = false;
        IsPaused = false;
        IgnoreNextFrame = false;
        TotalTimeTaken = 0;
        Health = 3;
        GameType = GameType.Standard;
        UsingKey = false;
    }

    public static void Reset(GameType type)
    {
        TimeRemaining = Constants.INITIAL_LEVEL_TIME;
        CurrentLevel = 0;
        CurrentDifficulty = Constants.STARTER_LEVEL_DIFFICULTY;
        Seed = -1;
        IsGameOver = false;
        IsPaused = false;
        IgnoreNextFrame = false;
        TotalTimeTaken = 0;
        Health = 3;
        GameType = type;
        UsingKey = false;
    }
}
