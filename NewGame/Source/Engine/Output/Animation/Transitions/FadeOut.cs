using System;

public class FadeOut : IAnimate
{
    private MyTimer timer;
    private int alpha;

    public FadeOut(int MSEC)
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
            alpha = 0;
        } else {
            alpha = (int)Math.Round(255 * ((float)timer.RemainingTime / (float)timer.MSec));
        }
        TARGET.color = new(TARGET.color, alpha);
    }

    public bool IsComplete() => timer.Test();
}