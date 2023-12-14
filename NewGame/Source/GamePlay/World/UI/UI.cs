using System;
using Microsoft.Xna.Framework;

public class UI
{
    private RoundEndOverlay endOverlay;

    private readonly Clickable homeBtn;

    public EventHandler<object> reset, openEditor, changeGameState;

    public UI(EventHandler<object> RESET, EventHandler<object> OPENEDITOR) 
    {
        reset = RESET;
        openEditor = OPENEDITOR;
        changeGameState = TransitionManager.ChangeGameState;
        
        homeBtn = new SpriteBuilder().WithPath("UI//Home")
                                    .WithAbsolutePosition(new Vector2(50, 50))
                                    .WithDims(new Vector2(50, 50))
                                    .WithButtonAction(changeGameState)
                                    .WithButtonInfo(GameState.MAIN_MENU)
                                    .WithUI(true)
                                    .BuildClickable();
    }

    public void Update() 
    {
        if (GameGlobals.roundState == RoundState.END) 
        {
            endOverlay ??= new RoundEndOverlay(reset, changeGameState, openEditor);
            endOverlay.Update();
        } else {
            endOverlay = null;
        }

        homeBtn.Update();
    }

    public void Draw()
    {
        if (Globals.gameState != GameState.MAIN_MENU)
        {
            homeBtn.Draw();
        }

        if (GameGlobals.roundState == RoundState.END && endOverlay != null 
                && (Globals.gameState == GameState.GAME_PLAY))
        {
            endOverlay.Draw();
        }
    }
}
