using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class RoundEndOverlay
{
    private readonly Button resetBtn;
    private readonly Button backBtn;

    public RoundEndOverlay(EventHandler<object> RESET, EventHandler<object> CHANGEGAMESTATE) 
    {
        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button220x32")
                                                        .WithDims(new Vector2(220, 32))
                                                        .WithColor(Colors.Buttons);
        resetBtn = buttonBuilder.WithOffset(new Vector2(-120, 200))
                            .WithText("Play Again")
                            .WithButtonAction(RESET)
                            .BuildButton();
        backBtn = buttonBuilder.WithOffset(new Vector2(120, 200))
                            .WithText("Main Menu")
                            .WithButtonAction(CHANGEGAMESTATE)
                            .WithButtonInfo(GameState.MAIN_MENU)
                            .BuildButton();
    }

    public void Update() 
    {
        resetBtn.Update();
        backBtn.Update();
    }

    public void Draw()
    {
        resetBtn.Draw();
        backBtn.Draw();
    }
}
