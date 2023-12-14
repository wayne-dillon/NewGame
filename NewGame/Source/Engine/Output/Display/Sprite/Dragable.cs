using System;
using Microsoft.Xna.Framework;

public class Dragable : Sprite
{
    public bool isHeld;
    public EventHandler<object> action;
    public object info;
    public Vector2 cursorOffset;

    public Dragable(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, IAnimate ANIMATION, EventHandler<object> ACTION, object INFO, bool ISTRANSITIONABLE, bool ISUI) 
        : base(PATH, ALIGNMENT, OFFSET, DIMS, COLOR, ANIMATION, InteractableType.NONE, ISTRANSITIONABLE, ISUI) 
    {
        action = ACTION;
        info = INFO;

        isHeld = false;
    }

    public override void Update()
    {
        if (Hover())
        {
            if (Globals.mouse.LeftClick())
            {
                isHeld = true;
                cursorOffset = Pos - Globals.mouse.newMousePos;
            }
        }
        if (Globals.mouse.LeftClickRelease())
        {
            isHeld = false;
        }

        if (isHeld)
        {
            Pos = Globals.mouse.newMousePos + cursorOffset;
        }

        base.Update();
    }

    protected void SkipOverUpdate()
    {
        base.Update();
    }

    public override void Draw()
    {
        base.Draw();
    }
}
