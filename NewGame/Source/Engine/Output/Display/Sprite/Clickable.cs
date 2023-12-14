using System;
using Microsoft.Xna.Framework;

public class Clickable : Sprite
{
    public bool isPressed, isHovered, isAvailable;
    public Color hoverColor, unavailableColor;
    public Vector2 hoverScale = new(1.1f, 1.1f);

    public object info;
    public event EventHandler<object> ButtonClicked;

    public Clickable(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, 
                    Color HOVERCOLOR, Color UNAVAILABLECOLOR, IAnimate ANIMATION, Vector2 HOVERSCALE, 
                    bool ISAVAILABLE, EventHandler<object> BUTTONCLICKED, object INFO, bool ISTRANSITIONABLE, bool ISUI) 
        : base(PATH, ALIGNMENT, OFFSET, DIMS, COLOR, ANIMATION, InteractableType.NONE, ISTRANSITIONABLE, ISUI) 
    {
        isAvailable = ISAVAILABLE;
        ButtonClicked = BUTTONCLICKED;
        info = INFO;
        hoverScale = HOVERSCALE;
        hoverColor = HOVERCOLOR;
        unavailableColor = UNAVAILABLECOLOR;

        isPressed = false;
    }

    public override void Update()
    {
        if (color.A != 255 || hoverColor.A != 255 || unavailableColor.A != 255)
        {
            hoverColor = new Color(hoverColor.R, hoverColor.G, hoverColor.B, color.A);
            unavailableColor = new Color(unavailableColor.R, unavailableColor.G, unavailableColor.B, color.A);
        }

        if (animation == null)
        {
            if (Hover())
            {
                isHovered = true;

                if (isAvailable)
                {
                    dims = baseDims * hoverScale;

                    if (Globals.mouse.LeftClick())
                    {
                        isHovered = false;
                        isPressed = true;
                    }
                    else if (Globals.mouse.LeftClickRelease())
                    {
                        OnButtonClicked();
                    }
                }
            }
            else
            {
                dims = baseDims;
                isHovered = false;
            }
        }

        if (!Globals.mouse.LeftClick() && !Globals.mouse.LeftClickHold())
        {
            isPressed = false;
        }

        base.Update();
    }

    public virtual void Reset()
    {
        isPressed = false;
        isHovered = false;
    }

    protected virtual void OnButtonClicked()
    {
        ButtonClicked?.Invoke(this, info);

        Reset();
        SoundEffects.PlayButtonClick();
    }

    public override void Draw()
    {
        Color tempColor = color;

        if (!isAvailable)
        {
            tempColor = unavailableColor;
        }
        else if (isPressed)
        {
            tempColor = Color.Gray;
        }
        else if (isHovered)
        {
            tempColor = hoverColor;
        }

        base.Draw(tempColor);
    }
}
