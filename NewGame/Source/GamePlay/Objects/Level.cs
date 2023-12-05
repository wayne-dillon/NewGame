using System.Collections.Generic;
using Microsoft.Xna.Framework;

public struct Level
{
    public List<Sprite> platforms;
    public List<Sprite> hazards;
    public List<Sprite> startBlocks;
    public List<AnimatedSprite> objectives;
    public Vector2 playerStartPos;
    public int top, bottom, left, right;

    public Level()
    {
        platforms = new();
        hazards = new();
        startBlocks = new();
        objectives = new();
        playerStartPos = Vector2.Zero;
        top = bottom = left = right = 0;
    }
}