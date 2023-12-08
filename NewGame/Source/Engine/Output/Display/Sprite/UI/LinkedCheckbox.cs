using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class LinkedCheckbox : Checkbox
{
    private List<LinkedCheckbox> linkedList;
    private TextComponent label;
    
    public LinkedCheckbox(string LABEL, Alignment ALIGNMENT, Vector2 OFFSET, IAnimate ANIMATION, EventHandler<object> BUTTONCLICKED, 
                            object INFO, bool ISTRANSITIONABLE, bool ISCHECKED, bool ISUI) 
        : base(ALIGNMENT, OFFSET, ANIMATION, BUTTONCLICKED, INFO, ISTRANSITIONABLE, ISCHECKED, ISUI) 
    {
        label = new TextComponentBuilder().WithText(LABEL)
                                        .WithScreenAlignment(ALIGNMENT)
                                        .WithTextAlignment(Alignment.CENTER_RIGHT)
                                        .WithOffset(OFFSET + new Vector2(-25, 0))
                                        .WithAnimation(ANIMATION)
                                        .Build();
    }

    public override void Update()
    {
        base.Update();
        label.Update();
    }

    protected override void OnButtonClicked()
    {
        if (linkedList == null || isChecked) 
        {
            return;
        }

        foreach (Checkbox box in linkedList)
        {
            box.SetChecked(false);
        }

        base.OnButtonClicked();
    }

    public void SetLinkedList(List<LinkedCheckbox> LIST)
    {
        linkedList = LIST;
    }

    public override void Draw()
    {
        base.Draw();
        label.Draw();
    }
}