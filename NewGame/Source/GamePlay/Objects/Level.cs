using System.Collections.Generic;
using Microsoft.Xna.Framework;

public struct Level
{
    public List<Sprite> platforms;
    public List<Sprite> hazards;
    public List<Sprite> start;
    public List<Sprite> end;
    public Vector2 playerStartPos;

    public Level()
    {
        platforms = new();
        hazards = new();
        start = new();
        end = new();
        playerStartPos = Vector2.Zero;
    }
}