using System;

public class GamePlay
{
    private readonly EventHandler<object> ChangeGameState;

    public GamePlay()
    {
        ChangeGameState = TransitionManager.ChangeGameState;
    }

    public virtual void Update()
    {
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
    }
}
