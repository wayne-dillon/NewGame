using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class GamePlay
{
    private readonly EventHandler<object> ChangeGameState;
    private Player player;
    private List<Sprite> platforms = new();

    public GamePlay()
    {
        player = new Player();

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

        ChangeGameState = TransitionManager.ChangeGameState;
    }

    public virtual void Update()
    {
        player.Update();
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
        GameGlobals.roundState = RoundState.START;
    }

    public void ResetAndChange(object SENDER, object INFO)
    {
        GameGlobals.roundState = RoundState.START;
        ChangeGameState(SENDER, INFO);
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
