using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private List<Sprite> platforms;

    public GamePlay()
    {
        Init();
    }

    private void Init()
    {
        player = new Player();
        camera = new Camera(player.sprite);
        platforms = new();

        SpriteBuilder platformBuilder = new SpriteBuilder().WithInteractableType(InteractableType.PLATFORM)
                                                        .WithPath("rect")
                                                        .WithColor(Colors.SealBrown);
        platforms.Add(platformBuilder.WithDims(new Vector2(1600, 20))
                                    .WithScreenAlignment(Alignment.TOP)
                                    .Build());
        platforms.Add(platformBuilder.WithScreenAlignment(Alignment.BOTTOM)
                                    .Build());
        platforms.Add(platformBuilder.WithDims(new Vector2(20, 900))
                                    .WithScreenAlignment(Alignment.CENTER_LEFT)
                                    .Build());
        platforms.Add(platformBuilder.WithScreenAlignment(Alignment.CENTER_RIGHT)
                                    .Build());
        platforms.Add(platformBuilder.WithDims(new Vector2(500, 100))
                                    .WithOffset(new Vector2(0, -50))
                                    .WithScreenAlignment(Alignment.BOTTOM)
                                    .Build());
    }

    public virtual void Update()
    {
        player.Update();
        camera.Update();
        foreach (Sprite platform in platforms)
        {
            platform.Update();
        }
        if (Globals.isNewGame) 
        {
            Globals.isNewGame = false;
            ResetWorld(null, null);
        }
    }

    public virtual void ResetWorld(object SENDER, object INFO)
    {
        Init();
    }

    public virtual void Draw()
    {
        foreach (Sprite platform in platforms)
        {
            platform.Draw();
        }
        player.Draw();
    }
}
