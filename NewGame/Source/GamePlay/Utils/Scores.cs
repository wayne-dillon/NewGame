using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

public class Scores
{
    private static List<RunDetails> levelScores;
    public static List<RunDetails> LevelScores
    {
        get
        {
            levelScores ??= new();
            return levelScores;
        }
    }

    private static string DocName() => "Source//Content//XML//Scores.xml";

    public static HighScoreDisplay GetHighScores(LevelSelection LEVEL, Alignment ALIGNMENT, Vector2 OFFSET)
    {
        List<RunDetails> orderedList = LevelScores.FindAll(o => o.level == LEVEL).OrderByDescending(o => o.score).ToList();

        return new HighScoreDisplay(orderedList, ALIGNMENT, OFFSET);
    }

    public static void ReadFromXML()
    {
        levelScores = new();
        XDocument xml = XDocument.Load(DocName());
        List<XElement> runList = (from t in xml.Element("scores").Descendants("runDetails")
                                    select t).ToList<XElement>();

        foreach (XElement run in runList)
        {
            int score = Convert.ToInt32(run.Element("score").Value);
            string player = run.Element("player").Value;
            int level = Convert.ToInt32(run.Element("level").Value);
            int day = Convert.ToInt32(run.Element("dateTime").Element("day").Value);
            int month = Convert.ToInt32(run.Element("dateTime").Element("month").Value);
            int year = Convert.ToInt32(run.Element("dateTime").Element("year").Value);

            DateTime date = new(year, month, day);

            levelScores.Add(new(score, player, (LevelSelection)level, date));
        }
    }

    public static void WriteToXML()
    {
        List<XElement> elements = new();

        foreach (RunDetails run in levelScores)
        {
            elements.Add(run.ReturnXML());
        }

        XDocument xml = new(new XDeclaration("1.0", "utf-8", null), new XElement("scores", elements));
        xml.Save(DocName());
    }
}