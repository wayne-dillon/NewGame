using System;
using Microsoft.Xna.Framework;

public class Checkbox : Clickable
{
    private ChildSprite check;
    protected bool isChecked;
    
    public Checkbox(Alignment ALIGNMENT, Vector2 OFFSET, IAnimate ANIMATION, EventHandler<object> BUTTONCLICKED, object INFO, bool ISTRANSITIONABLE, bool ISCHECKED, bool ISUI) 
        : base("UI//Oval20x20", ALIGNMENT, OFFSET, new Vector2(20,20), Colors.BaseUIElement, Colors.BaseUIElement, Colors.Unavailable, 
                ANIMATION, Vector2.One, true, BUTTONCLICKED, INFO, ISTRANSITIONABLE, ISUI) 
    {
        isChecked = ISCHECKED;
        check = new SpriteBuilder().WithPath("UI//OvalFill20x20").WithDims(dims).WithParent(this).BuildChild();
    }

    public override void Update()
    {
        base.Update();
        check.Update();
    }

    protected override void OnButtonClicked()
    {
        isChecked = !isChecked;
        base.OnButtonClicked();
    }

    public void SetChecked(bool CHECKED)
    {
        isChecked = CHECKED;
    }

    public override void Draw()
    {
        base.Draw();
        if (isChecked) {
            check.Draw();
        }
    }
}