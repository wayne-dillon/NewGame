using System.Collections.Generic;
using Microsoft.Xna.Framework;

public struct Level
{
    public Backdrop backdrop;
    public List<Sprite> platforms;
    public List<Sprite> hazards;
    public List<Sprite> startBlocks;
    public List<AnimatedSprite> objectives;
    public List<TextComponent> text;
    public Vector2 playerStartPos;
    public int top, bottom, left, right;

    public Level()
    {
        backdrop = new(EnumHelper.GetLevelBackdropPath(GameGlobals.currentLevel));
        platforms = new();
        hazards = new();
        startBlocks = new();
        objectives = new();
        text = new();
        playerStartPos = Vector2.Zero;
        top = bottom = left = right = 0;
    }
}