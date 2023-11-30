using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class LevelBuilder
{
    public static readonly int tileSize = 60;
    private static Level level;

    public static Level Build(string PATH)
    {
        level = new();
        List<string[]> input = CSVReader.ReadFile(PATH);

        Vector2 position = Vector2.Zero;
        foreach (string[] row in input)
        {
            foreach (string obj in row)
            {
                switch (EnumHelper.GetObject(obj))
                {
                    case LevelObject.PLATFORM:
                        CreatePlatform(position);
                        break;
                    case LevelObject.HAZARD:
                        CreateHazard(position);
                        break;
                    case LevelObject.START:
                        CreateStart(position);
                        break;
                    case LevelObject.END:
                        CreateEnd(position);
                        break;
                    case LevelObject.PLAYER:
                        level.playerStartPos = position;
                        break;
                    case LevelObject.EMPTY:
                    default:
                        break;
                }
                position.X += tileSize;
            }
            position = new Vector2(0, position.Y + tileSize);
        }

        return level;
    }

    private static void CreatePlatform(Vector2 POS)
    {
        level.platforms.Add(new SpriteBuilder().WithInteractableType(InteractableType.PLATFORM)
                                                        .WithPath("rect")
                                                        .WithColor(Colors.SealBrown)
                                                        .WithAbsolutePosition(POS)
                                                        .WithDims(new Vector2(tileSize, tileSize))
                                                        .Build());
    }

    private static void CreateHazard(Vector2 POS)
    {
        level.hazards.Add(new SpriteBuilder().WithInteractableType(InteractableType.HAZARD)
                                                        .WithPath("rect")
                                                        .WithColor(Color.Red)
                                                        .WithAbsolutePosition(POS)
                                                        .WithDims(new Vector2(tileSize, tileSize))
                                                        .Build());
    }

    private static void CreateStart(Vector2 POS)
    {
        level.start.Add(new SpriteBuilder().WithInteractableType(InteractableType.START_TIMER)
                                    .WithPath("rect")
                                    .WithColor(Colors.ShamrockGreen)
                                    .WithAbsolutePosition(POS)
                                    .WithDims(new Vector2(tileSize, tileSize))
                                    .Build());
    }

    private static void CreateEnd(Vector2 POS)
    {
        level.end.Add(new SpriteBuilder().WithInteractableType(InteractableType.END_ROUND)
                                    .WithPath("rect")
                                    .WithColor(Colors.AmaranthPurple)
                                    .WithAbsolutePosition(POS)
                                    .WithDims(new Vector2(tileSize, tileSize))
                                    .Build());
    }
}