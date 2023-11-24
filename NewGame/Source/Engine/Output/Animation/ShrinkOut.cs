using Microsoft.Xna.Framework;

public class ShrinkOut : IAnimate
{
    private MyTimer timer;

    public ShrinkOut(int MSEC)
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
            TARGET.dims = Vector2.Zero;
        } else {
            TARGET.dims = new Vector2(TARGET.baseDims.X * ((float)timer.RemainingTime / (float)timer.MSec), 
                                    TARGET.baseDims.Y * ((float)timer.RemainingTime / (float)timer.MSec));
        }
    }

    public bool IsComplete() => timer.Test();
}