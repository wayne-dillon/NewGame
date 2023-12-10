using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class HighScoreDisplay
{
    private List<TextComponent> scoreDetails = new();
    private List<TextComponent> scores = new();

    public HighScoreDisplay(List<RunDetails> DETAILS, Alignment ALIGNMENT, Vector2 OFFSET)
    {
        int totalScores = DETAILS.Count;
        TextComponentBuilder detailsBuilder = new TextComponentBuilder().WithTextAlignment(Alignment.CENTER_LEFT)
                                                                        .WithScreenAlignment(ALIGNMENT);
        TextComponentBuilder scoreBuilder = new TextComponentBuilder().WithFont(Fonts.numberFont)
                                                                    .WithTextAlignment(Alignment.CENTER_RIGHT)
                                                                    .WithScreenAlignment(ALIGNMENT);

        for (int i = 0; i < 5; i++)
        {
            int yOffset = (i - 2) * 20;
            if (i < totalScores)
            {
                scoreDetails.Add(detailsBuilder.WithText($"{DETAILS[i].dateTime:dd/MM/yyyy}  {DETAILS[i].player}")
                                            .WithOffset(OFFSET + new Vector2(-300, yOffset))
                                            .Build());
                scores.Add(scoreBuilder.WithText($"{DETAILS[i].score:D5}")
                                        .WithOffset(OFFSET + new Vector2(300, yOffset))
                                        .Build());
            } else {
                scoreDetails.Add(detailsBuilder.WithText("--/--/----    ---")
                                            .WithOffset(OFFSET + new Vector2(-300, yOffset))
                                            .Build());
                scores.Add(scoreBuilder.WithText("-----")
                                        .WithOffset(OFFSET + new Vector2(300, yOffset))
                                        .Build());
            }
        }
    }

    public void Update()
    {
        foreach (TextComponent text in scoreDetails)
        {
            text.Update();
        }
        foreach (TextComponent text in scores)
        {
            text.Update();
        }
    }

    public void Draw()
    {
        foreach (TextComponent text in scoreDetails)
        {
            text.Draw();
        }
        foreach (TextComponent text in scores)
        {
            text.Draw();
        }
    }
}