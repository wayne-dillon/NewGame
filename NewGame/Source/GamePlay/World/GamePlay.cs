using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private Player player;
    private Camera camera;
    private List<Sprite> platforms;
    private List<Sprite> hazards;
    private Sprite startLine;
    private Sprite finishLine;

    public GamePlay()
    {
        Init();
    }

    private void Init()
    {
        Platforms.Reset();
        Hazards.Reset();

        player = new Player();
        camera = new Camera(player.sprite);
        platforms = new();
        hazards = new();

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
                                    
        SpriteBuilder hazardBuilder = new SpriteBuilder().WithInteractableType(InteractableType.HAZARD)
                                                        .WithPath("rect")
                                                        .WithColor(Color.Red);
        hazards.Add(hazardBuilder.WithDims(new Vector2(4000, 20))
                                    .WithScreenAlignment(Alignment.TOP)
                                    .WithOffset(new Vector2(0, -500))
                                    .Build());
        hazards.Add(hazardBuilder.WithScreenAlignment(Alignment.BOTTOM)
                                    .WithOffset(new Vector2(0, 500))
                                    .Build());
        hazards.Add(hazardBuilder.WithDims(new Vector2(20, 3000))
                                    .WithScreenAlignment(Alignment.CENTER_LEFT)
                                    .WithOffset(new Vector2(-500, 0))
                                    .Build());
        hazards.Add(hazardBuilder.WithScreenAlignment(Alignment.CENTER_RIGHT)
                                    .WithOffset(new Vector2(500, 0))
                                    .Build());

        GameGlobals.roundState = RoundState.START;
    }

    public virtual void Update()
    {
        player.Update();
        camera.Update();
        foreach (Sprite platform in platforms)
        {
            platform.Update();
        }
        foreach (Sprite hazard in hazards)
        {
            hazard.Update();
        }
        if (GameGlobals.roundState == RoundState.START)
        {
            //check for collision with start line
        }
        if (GameGlobals.roundState == RoundState.TIMER_RUNNING)
        {
            //check for collision with end line
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
        foreach (Sprite hazard in hazards)
        {
            hazard.Draw();
        }
        foreach (Sprite platform in platforms)
        {
            platform.Draw();
        }
        player.Draw();
    }
}
