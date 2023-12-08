using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class LinkedButton : Button
{
    private List<LinkedButton> linkedList;
    
    public LinkedButton(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, Color HOVERCOLOR, Color UNAVAILABLECOLOR,
                Vector2 HOVERSCALE, IAnimate ANIMATION, string TEXT, SpriteFont FONT, bool ISAVAILABLE, EventHandler<object> BUTTONCLICKED,
                object INFO, bool ISTRANSITIONABLE, bool ISUI) 
        : base(PATH, ALIGNMENT, OFFSET, DIMS, COLOR, HOVERCOLOR, UNAVAILABLECOLOR, HOVERSCALE, ANIMATION, TEXT, FONT, ISAVAILABLE, BUTTONCLICKED,
                INFO, ISTRANSITIONABLE, ISUI)
    {
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnButtonClicked()
    {
        if (linkedList == null || !isAvailable) 
        {
            return;
        }

        foreach (LinkedButton button in linkedList)
        {
            button.isAvailable = true;
        }
        isAvailable = false;

        base.OnButtonClicked();
    }

    public void SetLinkedList(List<LinkedButton> LIST)
    {
        linkedList = LIST;
    }

    public override void Draw()
    {
        base.Draw();
    }
}