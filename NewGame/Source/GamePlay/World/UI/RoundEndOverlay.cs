using System;
using Microsoft.Xna.Framework;

public class RoundEndOverlay
{
    private readonly Button lvlEditorBtn;
    private readonly Button nextBtn;
    private readonly Button resetBtn;
    private readonly Button backBtn;
    private readonly EventHandler<object> changeGameState;
    private readonly EventHandler<object> openEditor;
    private readonly EventHandler<object> reset;
    private HighScoreDisplay highScoreDisplay;
    private OutroBackdrop outroBackdrop;

    public RoundEndOverlay(EventHandler<object> RESET, EventHandler<object> CHANGEGAMESTATE, EventHandler<object> OPENEDITOR) 
    {
        changeGameState = CHANGEGAMESTATE;
        openEditor = OPENEDITOR;
        reset = RESET;

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button727x112")
                                                        .WithDims(new Vector2(243, 37))
                                                        .WithUI(true);
        resetBtn = buttonBuilder.WithOffset(new Vector2(0, 150))
                            .WithText("Play Again")
                            .WithButtonAction(reset)
                            .BuildButton();
        nextBtn = buttonBuilder.WithOffset(new Vector2(0, 100))
                            .WithText("Next Level")
                            .WithButtonInfo(true)
                            .BuildButton();
        backBtn = buttonBuilder.WithOffset(new Vector2(0, 200))
                            .WithText("Main Menu")
                            .WithButtonAction(changeGameState)
                            .WithButtonInfo(GameState.MAIN_MENU)
                            .BuildButton();
        lvlEditorBtn = buttonBuilder.WithOffset(new Vector2(0, 100))
                            .WithText("Level Editor")
                            .WithButtonAction(openEditor)
                            .BuildButton();
        
        if (GameGlobals.currentLevel != LevelSelection.TUTORIAL)
        {
            highScoreDisplay = Scores.GetHighScores(GameGlobals.currentLevel, Alignment.CENTER, Vector2.Zero);
        }
    }

    public void Update() 
    {
        if (GameGlobals.beatLevel && GameGlobals.currentLevel == LevelSelection.LEVEL_3
                && Persistence.preferences.displayOutro)
        {
            outroBackdrop = new OutroBackdrop();
            Persistence.preferences.displayOutro = false;
            Persistence.SavePreferences();
        }

        outroBackdrop?.Update();

        if (GameGlobals.beatLevel)
        {
            if (GameGlobals.currentLevel == LevelSelection.LEVEL_3)
            {
                lvlEditorBtn.Update();
            } else {
                nextBtn.Update();
            }
        }
        if (GameGlobals.currentLevel != LevelSelection.TUTORIAL)
        {
            highScoreDisplay?.Update();
        }
        resetBtn.Update();
        backBtn.Update();
        if (InputController.Confirm()) reset(null, null);
        if (InputController.Back()) changeGameState(null, GameState.MAIN_MENU);
    }

    public void Draw()
    {
        outroBackdrop?.Draw();

        if (outroBackdrop == null || !outroBackdrop.IsPlaying())
        {
            if (GameGlobals.beatLevel)
            {
                if (GameGlobals.currentLevel == LevelSelection.LEVEL_3)
                {
                    lvlEditorBtn.Draw();
                } else {
                    nextBtn.Draw();
                }
            }
            if (GameGlobals.currentLevel != LevelSelection.TUTORIAL)
            {
                highScoreDisplay?.Draw();
            }
            resetBtn.Draw();
            backBtn.Draw();
        }
    }
}
