using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Button : Clickable
{
    private readonly TextComponent text;

    public Button(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, Color HOVERCOLOR, Color UNAVAILABLECOLOR,
                Vector2 HOVERSCALE, IAnimate ANIMATION, string TEXT, SpriteFont FONT, bool ISAVAILABLE, EventHandler<object> BUTTONCLICKED,
                object INFO, bool ISTRANSITIONABLE, bool ISUI) 
        : base(PATH, ALIGNMENT, OFFSET, DIMS, COLOR, HOVERCOLOR, UNAVAILABLECOLOR, ANIMATION, HOVERSCALE, ISAVAILABLE, BUTTONCLICKED, INFO, ISTRANSITIONABLE, ISUI) 
    {
        text = new TextComponentBuilder().WithText(TEXT).WithFont(FONT).WithScreenAlignment(ALIGNMENT).WithOffset(OFFSET).Build();
    }

    public override void Update()
    {
        base.Update();
        text.Update();
    }

    public override void Draw()
    {
        base.Draw();
        text.Draw();
    }
}
