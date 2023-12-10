using System;

public class TransitionManager
{
    private static GameState transitionToState;
    private static GameState transitionFromState;
    public static TransitionState transState = TransitionState.BEGIN_IN;

    public static readonly int transTime = 250;
    private static int waitTime;

    private static IAnimate animation;
    public static Action<Animatable> animate;
    
    public static void Update()
    {
        animation?.Update();

        switch (transState)
        {
            case TransitionState.BEGIN_OUT:
                transState = TransitionState.PLAYING_OUT;
                animation = new FadeOut(transTime);
                animate = animation.Animate;
                Music.SetFadeTime(transTime);
                return;
            case TransitionState.PLAYING_OUT:
                if (transitionToState == GameState.GAME_PLAY || Globals.gameState == GameState.GAME_PLAY) 
                    Music.FadeDown();
                if (animation.IsComplete())
                {
                    waitTime = transTime;
                    transState = TransitionState.PAUSE;
                    animation = null;
                }
                return;
            case TransitionState.PAUSE:
                if (waitTime > 0) 
                {
                    waitTime -= Globals.gameTime.ElapsedGameTime.Milliseconds;
                } else 
                {
                    transitionFromState = Globals.gameState;
                    Globals.gameState = transitionToState;
                    Globals.isNewGame = true;
                    transState = TransitionState.BEGIN_IN;
                    if (transitionFromState == GameState.GAME_PLAY || Globals.gameState == GameState.GAME_PLAY)
                        Music.SetTrack();
                }
                return;
            case TransitionState.BEGIN_IN:
                transState = TransitionState.PLAYING_IN;
                animation = new FadeIn(transTime);
                animate = animation.Animate;
                Music.SetFadeTime(transTime);
                return;
            case TransitionState.PLAYING_IN:
                if (transitionFromState == GameState.GAME_PLAY || Globals.gameState == GameState.GAME_PLAY) 
                    Music.FadeUp();
                if (animation.IsComplete())
                {
                    transState = TransitionState.SET;
                    animation = null;
                }
                return;
        }
    }
    
    public static void ChangeGameState(object SENDER, object INFO)
    {
        if (INFO is GameState state)
        {
            transitionToState = state;
            transState = TransitionState.OUT_REQUESTED;
        }
    }

}