using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class ModeIndicator
{
    private AnimatedSprite prev;
    private AnimatedSprite current;
    private AnimatedSprite next;

    private List<Sprite> backgrounds = new();

    private Dictionary<int, string> spriteDict = new()
    {
        { 0, "Symbols//monkeySymbol" },
        { 1, "Symbols//frogSymbol" },
        { 2, "Symbols//baseMode" }
    };

    public ModeIndicator()
    {
        SpriteBuilder builder = new SpriteBuilder().WithPathDict(spriteDict).WithScreenAlignment(Alignment.TOP).WithUI(true);
        prev = builder.WithDims(new Vector2(50,50)).WithOffset(new Vector2(-100, 50)).BuildAnimated();
        next = builder.WithOffset(new Vector2(100, 50)).BuildAnimated();
        current = builder.WithDims(new Vector2(100,100)).WithOffset(new Vector2(0, 50)).BuildAnimated();

        SpriteBuilder bkgBuilder = new SpriteBuilder().WithPath("UI//Circle208x208").WithScreenAlignment(Alignment.TOP).WithUI(true);
        backgrounds.Add(bkgBuilder.WithDims(new Vector2(50,50)).WithOffset(new Vector2(-100, 50)).Build());
        backgrounds.Add(bkgBuilder.WithOffset(new Vector2(100,50)).Build());
        backgrounds.Add(bkgBuilder.WithDims(new Vector2(100, 100)).WithOffset(new Vector2(0,50)).Build());
    }

    public void Update()
    {
        foreach (Sprite sprite in backgrounds)
        {
            sprite.Update();
        }

        int currentModeIndex = (int)GameGlobals.currentMode;
        int prevModeIndex = GameGlobals.currentMode == CharacterMode.MONKEY ? 2 : currentModeIndex - 1;
        int nextModeIndex = GameGlobals.currentMode == CharacterMode.CAT ? 0 : currentModeIndex + 1;
        prev.SetAnimationValues(prevModeIndex, prevModeIndex, 1);
        next.SetAnimationValues(nextModeIndex, nextModeIndex, 1);
        current.SetAnimationValues(currentModeIndex, currentModeIndex, 1);

        prev.Update();
        current.Update();
        next.Update();
    }

    public void Draw()
    {
        foreach (Sprite sprite in backgrounds)
        {
            sprite.Draw();
        }

        prev.Draw();
        current.Draw();
        next.Draw();
    }
}