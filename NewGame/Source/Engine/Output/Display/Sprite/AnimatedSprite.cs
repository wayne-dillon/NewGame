using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimatedSprite : Sprite
{
    private Dictionary<int, Texture2D> dict = new();

    private MyTimer frameTimer;
    private int currentFrame;
    private int rangeMin;
    private int rangeMax;

    public AnimatedSprite(Dictionary<int, string> PATHDICT, int FRAMETIME, int RANGEMIN, int RANGEMAX, Alignment ALIGNMENT, 
            Vector2 OFFSET, Vector2 DIMS, Color COLOR, IAnimate ANIMATION, InteractableType TYPE, bool ISTRANSITIONABLE)
        : base("rect", ALIGNMENT, OFFSET, DIMS, COLOR, ANIMATION, TYPE, ISTRANSITIONABLE)
    {
        foreach (KeyValuePair<int, string> path in PATHDICT)
        {
            dict.Add(path.Key, Globals.content.Load<Texture2D>(path.Value));
        }
        frameTimer = new MyTimer(FRAMETIME);
        rangeMin = RANGEMIN;
        rangeMax = RANGEMAX;
        currentFrame = rangeMin;
        myModel = dict[currentFrame];
    }

    public override void Update()
    {
        frameTimer.UpdateTimer();
        if (frameTimer.Test())
        {
            currentFrame++;
            if (currentFrame > rangeMax) currentFrame = rangeMin;
            myModel = dict[currentFrame];
            frameTimer.ResetToZero();
        }
        base.Update();
    }

    public void SetRange(int MIN, int MAX)
    {
        rangeMin = MIN;
        rangeMax = MAX;
        currentFrame = rangeMin;
        frameTimer.ResetToZero();
    }

    public override void Draw()
    {
        base.Draw();
    }
}