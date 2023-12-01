using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class LevelBuilder
{
    public static readonly int tileSize = 60;
    private static Level level;

    private static Dictionary<int, string> objectiveDict = new()
    {
        { 0, "Symbols//airplaneModePhone" },
        { 1, "Symbols//ringingPhone1" },
        { 2, "Symbols//ringingPhone2" },
        { 3, "Symbols//ringingPhone3" },
        { 4, "Symbols//ringingPhone4" },
        { 5, "Symbols//ringingPhone3" },
        { 6, "Symbols//ringingPhone2" }
    };

    public static Level Build(string PATH)
    {
        level = new();
        List<string[]> input = CSVReader.ReadFile(PATH);

        Vector2 position = Vector2.Zero;
        foreach (string[] row in input)
        {
            foreach (string obj in row)
            {
                LevelObject levelObject = EnumHelper.GetObject(obj);
                switch (levelObject)
                {
                    case LevelObject.PLATFORM_BOTTOM:
                    case LevelObject.PLATFORM_BOTTOM_LEFT:
                    case LevelObject.PLATFORM_BOTTOM_RIGHT:
                    case LevelObject.PLATFORM_HORIZONTAL:
                    case LevelObject.PLATFORM_LEFT:
                    case LevelObject.PLATFORM_OPEN:
                    case LevelObject.PLATFORM_OPEN_BOTTOM:
                    case LevelObject.PLATFORM_OPEN_LEFT:
                    case LevelObject.PLATFORM_OPEN_RIGHT:
                    case LevelObject.PLATFORM_OPEN_TOP:
                    case LevelObject.PLATFORM_RIGHT:
                    case LevelObject.PLATFORM_SINGLE:
                    case LevelObject.PLATFORM_TOP:
                    case LevelObject.PLATFORM_TOP_LEFT:
                    case LevelObject.PLATFORM_TOP_RIGHT:
                    case LevelObject.PLATFORM_VERTICAL:
                        CreatePlatform(levelObject, position);
                        break;
                    case LevelObject.HAZARD:
                        CreateHazard(position);
                        break;
                    case LevelObject.START:
                        CreateStart(position);
                        break;
                    case LevelObject.OBJECTIVE:
                        CreateObjective(position);
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

    private static void CreatePlatform(LevelObject obj, Vector2 POS)
    {
        level.platforms.Add(new SpriteBuilder().WithInteractableType(InteractableType.PLATFORM)
                                                        .WithPath(EnumHelper.GetPlatformPath(obj))
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
        level.startBlocks.Add(new SpriteBuilder().WithInteractableType(InteractableType.START_TIMER)
                                    .WithPath("Symbols//Checker")
                                    .WithAbsolutePosition(POS)
                                    .WithDims(new Vector2(tileSize, tileSize))
                                    .Build());
    }

    private static void CreateObjective(Vector2 POS)
    {
        level.objectives.Add(new SpriteBuilder().WithInteractableType(InteractableType.OBJECTIVE)
                                    .WithPathDict(objectiveDict)
                                    .WithFrameTime(100)
                                    .WithRangeMinMax(1,6)
                                    .WithAbsolutePosition(POS)
                                    .WithDims(new Vector2(tileSize, tileSize))
                                    .BuildAnimated());
    }
}