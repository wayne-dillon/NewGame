﻿using System;
using Microsoft.Xna.Framework;

public class RoundEndOverlay
{
    private readonly Button nextBtn;
    private readonly Button resetBtn;
    private readonly Button backBtn;
    private readonly EventHandler<object> changeGameState;
    private readonly EventHandler<object> reset;

    public RoundEndOverlay(EventHandler<object> RESET, EventHandler<object> CHANGEGAMESTATE) 
    {
        changeGameState = CHANGEGAMESTATE;
        reset = RESET;

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button220x32")
                                                        .WithDims(new Vector2(220, 32))
                                                        .WithColor(Colors.Buttons)
                                                        .WithUI(true);
        resetBtn = buttonBuilder.WithOffset(new Vector2(-120, 200))
                            .WithText("Play Again")
                            .WithButtonAction(reset)
                            .BuildButton();
        nextBtn = buttonBuilder.WithOffset(new Vector2(0, 150))
                            .WithText("Next Level")
                            .WithButtonInfo(true)
                            .BuildButton();
        backBtn = buttonBuilder.WithOffset(new Vector2(120, 200))
                            .WithText("Main Menu")
                            .WithButtonAction(changeGameState)
                            .WithButtonInfo(GameState.MAIN_MENU)
                            .BuildButton();
    }

    public void Update() 
    {
        if (GameGlobals.beatLevel && GameGlobals.currentLevel != LevelSelection.LEVEL_4)
        {
            nextBtn.Update();
        }
        resetBtn.Update();
        backBtn.Update();
        if (InputController.Confirm()) reset(null, null);
        if (InputController.Back()) changeGameState(null, GameState.MAIN_MENU);
    }

    public void Draw()
    {
        if (GameGlobals.beatLevel && GameGlobals.currentLevel != LevelSelection.LEVEL_4)
        {
            nextBtn.Draw();
        }
        resetBtn.Draw();
        backBtn.Draw();
    }
}
