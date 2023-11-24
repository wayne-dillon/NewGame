using System;

public class FadeIn : IAnimate
{
    private MyTimer timer;
    private int alpha;

    public FadeIn(int MSEC)
    {
        timer = new MyTimer(MSEC);
    }

    public void Update()
    {
        timer.UpdateTimer();
    }

    public void Animate(Animatable TARGET)
    {
        if (IsComplete())
        {
            alpha = 255;
        } else {
            alpha = (int)Math.Round(255 * ((float)timer.Timer / (float)timer.MSec));
        }
        TARGET.color = new(TARGET.color, alpha);
    }

    public bool IsComplete() => timer.Test();
}