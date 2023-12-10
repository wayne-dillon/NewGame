using System;

public struct RunDetails
{
    public int score;
    public string player;
    public LevelSelection level;
    public DateTime dateTime;

    public RunDetails(int SCORE, string PLAYER, LevelSelection LEVEL, DateTime DATETIME)
    {
        score = SCORE;
        player = PLAYER;
        level = LEVEL;
        dateTime = DATETIME;
    }
}