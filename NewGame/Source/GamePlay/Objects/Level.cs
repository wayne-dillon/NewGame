using System.Collections.Generic;
using Microsoft.Xna.Framework;

public struct Level
{
    public List<Sprite> platforms;
    public List<Sprite> hazards;
    public List<Sprite> startBlocks;
    public List<Sprite> objectives;
    public Vector2 playerStartPos;

    public Level()
    {
        platforms = new();
        hazards = new();
        startBlocks = new();
        objectives = new();
        playerStartPos = Vector2.Zero;
    }
}