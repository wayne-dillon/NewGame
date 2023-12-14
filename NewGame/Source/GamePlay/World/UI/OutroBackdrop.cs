using Microsoft.Xna.Framework;

public class OutroBackdrop
{
    private Sprite endScreen1;
    private Sprite endScreen2;
    private MyTimer timer1;
    private MyTimer timer2;
    private bool trans2 = true;

    public OutroBackdrop()
    {
        SpriteBuilder builder = new SpriteBuilder().WithDims(new Vector2(Coordinates.screenWidth, Coordinates.screenHeight))
                                                .WithUI(true)
                                                .WithTransitionable(false)
                                                .WithColor(new(255,255,255,0));
        endScreen2 = builder.WithPath("Background//EndScreen2").Build();
        endScreen1 = builder.WithPath("Background//EndScreen1").WithAnimation(new FadeIn(250)).Build();
        timer1 = new(3000);
        timer2 = new(5000);
    }

    public void Update()
    {
        timer1.UpdateTimer();
        timer2.UpdateTimer();
        endScreen1.Update();
        endScreen2.Update();
        if (trans2 && timer1.Test())
        {
            trans2 = false;
            endScreen2.animation = new FadeIn(250);
        }
    }

    public bool IsPlaying()
    {
        return !timer2.Test();
    }

    public void Draw()
    {
        endScreen1.Draw();
        endScreen2.Draw();
    }
}