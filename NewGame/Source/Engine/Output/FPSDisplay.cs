using Microsoft.Xna.Framework;

public class FPSDisplay
{
    private int fps;
    private int frameCount = 0;
    private MyTimer timer;
    private TextComponent fpsDisplay;

    public FPSDisplay() 
    {
        timer = new MyTimer(1000, true);
        fpsDisplay = new TextComponentBuilder().WithScreenAlignment(Alignment.BOTTOM_LEFT)
                                            .WithTextAlignment(Alignment.CENTER_LEFT)
                                            .WithOffset(new Vector2(50, -50))
                                            .WithTransitionable(false)
                                            .Build();
    }

    public void Update() 
    {
    }

    public void Draw() 
    {
        timer.UpdateTimer();
        if (timer.Test()) {
            fps = frameCount;
            frameCount = 0;
            timer.ResetToZero();
        } else {
            frameCount++;
        }
        fpsDisplay.Update("FPS: " + fps);

        fpsDisplay.Draw();
    }
}