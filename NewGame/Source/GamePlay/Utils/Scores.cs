using System.Collections.Generic;
using System.Linq;
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

    public static HighScoreDisplay GetHighScores(LevelSelection LEVEL, Alignment ALIGNMENT, Vector2 OFFSET)
    {
        List<RunDetails> orderedList = levelScores.FindAll(o => o.level == LEVEL).OrderByDescending(o => o.score).ToList();

        return new HighScoreDisplay(orderedList, ALIGNMENT, OFFSET);
    }
}