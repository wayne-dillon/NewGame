using System;
using Microsoft.Xna.Framework;

public class UI
{
    private RoundEndOverlay endOverlay;

    private readonly FPSDisplay fpsDisplay;
    
    private readonly Clickable homeBtn;

    public EventHandler<object> reset, changeGameState;

    public UI(EventHandler<object> RESET) 
    {
        reset = RESET;
        changeGameState = TransitionManager.ChangeGameState;
        
        homeBtn = new SpriteBuilder().WithPath("UI//Home")
                                    .WithAbsolutePosition(new Vector2(30, 30))
                                    .WithDims(new Vector2(32, 32))
                                    .WithButtonAction(changeGameState)
                                    .WithButtonInfo(GameState.MAIN_MENU)
                                    .WithUI(true)
                                    .BuildClickable();

        fpsDisplay = new FPSDisplay();
    }

    public void Update() 
    {
        if (GameGlobals.roundState == RoundState.END) 
        {
            endOverlay ??= new RoundEndOverlay(reset, changeGameState);
            endOverlay.Update();
        } else {
            endOverlay = null;
        }

        homeBtn.Update();
        fpsDisplay.Update();
    }

    public void Draw()
    {
        if (Globals.gameState != GameState.MAIN_MENU)
        {
            homeBtn.Draw();
        }
        fpsDisplay.Draw();

        if (GameGlobals.roundState == RoundState.END && endOverlay != null 
                && (Globals.gameState == GameState.GAME_PLAY))
        {
            endOverlay.Draw();
        }
    }
}
