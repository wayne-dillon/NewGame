using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Backdrop
{
    public readonly List<Sprite> windowViews = new();
    private readonly Sprite staticBackground;
    public IAnimate animation = new ScrollHorizontal(-0.03f);

    public Backdrop(string VIEW_PATH)
    {
        SpriteBuilder spriteBuilder = new SpriteBuilder().WithTransitionable(false).WithUI(true);

        staticBackground = spriteBuilder.WithPath("Background//staticBackdrop")
                                        .WithDims(new Vector2(1920, 1080))
                                        .Build();

        windowViews.Add(spriteBuilder.WithAbsolutePosition(new Vector2(0, 250))
                                    .WithPath(VIEW_PATH)
                                    .WithDims(new Vector2(3840, 582))
                                    .Build());
        windowViews.Add(spriteBuilder.WithAbsolutePosition(new Vector2(3840, 250))
                                    .WithDims(new Vector2(3840, 582))
                                    .Build());

    }

    public void Update()
    {
        foreach (Sprite symbol in windowViews)
        {
            animation.Animate(symbol);
        }
    }

    public void Draw()
    {
        foreach (Sprite symbol in windowViews)
        {
            symbol.Draw();
        }
        staticBackground.Draw();
    }
}