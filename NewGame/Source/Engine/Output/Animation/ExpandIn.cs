using Microsoft.Xna.Framework;

public class ExpandIn : IAnimate
{
    private MyTimer timer;

    public ExpandIn(int MSEC)
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
            TARGET.dims = TARGET.baseDims;
        } else {
            TARGET.dims = new Vector2(TARGET.baseDims.X * ((float)timer.Timer / (float)timer.MSec), 
                                    TARGET.baseDims.Y * ((float)timer.Timer / (float)timer.MSec));
        }
    }

    public bool IsComplete() => timer.Test();
}