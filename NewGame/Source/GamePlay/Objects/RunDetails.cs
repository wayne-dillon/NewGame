using System;
using System.Xml.Linq;

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

    public XElement ReturnXML() => new("runDetails",
            new XElement("score", score),
            new XElement("player", player),
            new XElement("level", (int)level),
            new XElement("dateTime",
                new XElement("day", dateTime.Day),
                new XElement("month", dateTime.Month),
                new XElement("year", dateTime.Year))
            );
}