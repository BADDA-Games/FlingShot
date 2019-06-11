using System;
public static class GameVariables
{
    public static float TimeRemaining;
    public static int CurrentLevel;
    public static int Seed;
    public static bool IsGameOver;
    public static int TotalTimeTaken;

    public static void Reset(int initialTimeRemaining)
    {
        TimeRemaining = initialTimeRemaining;
        CurrentLevel = 0;
        Seed = -1;
        IsGameOver = false;
        TotalTimeTaken = 0;
    }
}
