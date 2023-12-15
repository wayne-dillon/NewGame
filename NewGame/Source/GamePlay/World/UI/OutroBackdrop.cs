using Microsoft.Xna.Framework;

public class OutroBackdrop
{
    private Sprite endScreen1;
    private Sprite endScreen2;
    private Sprite black;
    private MyTimer transTimer;
    private int transStage = 0;
    private int fadeTime = 250;
    private int waitTime = 4000;

    public OutroBackdrop()
    {
        SpriteBuilder builder = new SpriteBuilder().WithDims(new Vector2(Coordinates.screenWidth, Coordinates.screenHeight))
                                                .WithUI(true)
                                                .WithTransitionable(false)
                                                .WithColor(new(255,255,255,0));
        endScreen2 = builder.WithPath("Background//EndScreen2").Build();
        endScreen1 = builder.WithPath("Background//EndScreen1").Build();
        black = builder.WithPath("rect").WithColor(Color.Black).WithAnimation(new FadeIn(fadeTime)).Build();
        transTimer = new(fadeTime);
    }

    public void Update()
    {
        transTimer.UpdateTimer();
        endScreen1.Update();
        endScreen2.Update();
        black.Update();

        if (transTimer.Test())
        {
            switch (transStage)
            {
                case 0:
                    endScreen1.color = Color.White;
                    black.animation = new FadeOut(fadeTime);
                    transTimer.Reset(waitTime);
                    transStage++;
                    break;
                case 1:
                    black.animation = new FadeIn(fadeTime);
                    transTimer.Reset(fadeTime);
                    transStage++;
                    break;
                case 2:
                    endScreen2.color = Color.White;
                    black.animation = new FadeOut(fadeTime);
                    transTimer.Reset(waitTime);
                    transStage++;
                    break;
                case 3:
                    transStage++;
                    break;
                default:
                    break;
            }
        }
    }

    public bool IsPlaying()
    {
        return transStage != 4;
    }

    public void Draw()
    {
        endScreen1.Draw();
        endScreen2.Draw();
        black.Draw();
    }
}