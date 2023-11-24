using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SpriteBuilder
{
    private string Path, Text = "";
    private SpriteFont Font;
    private Vector2 Offset, Dims, HoverScale = new(1.1f, 1.1f);
    private Alignment ScreenAlignment = Alignment.CENTER;
    private Color Color = Color.White;
    private Color HoverColor = Color.AliceBlue;
    private Color UnavailableColor = Colors.Unavailable;
    private IAnimate Animation;
    private EventHandler<object> ButtonAction;
    private object ButtonInfo;
    private Sprite Parent;
    private bool IsAvailable = true;
    private bool IsTransitionable = true;
    private bool IsChecked;

    public Sprite Build() => new(Path, ScreenAlignment, Offset, Dims, Color, Animation, IsTransitionable);

    public Button BuildButton() => new(Path, ScreenAlignment, Offset, Dims, Color, HoverColor, 
                UnavailableColor, HoverScale, Animation, Text, Font, IsAvailable, ButtonAction, ButtonInfo, IsTransitionable);

    public LinkedButton BuildLinkedButton() => new(Path, ScreenAlignment, Offset, Dims, Color, HoverColor, 
                UnavailableColor, HoverScale, Animation, Text, Font, IsAvailable, ButtonAction, ButtonInfo, IsTransitionable);

    public Checkbox BuildCheckbox() => new(ScreenAlignment, Offset, Animation, ButtonAction, ButtonInfo, IsTransitionable, IsChecked);

    public LinkedCheckbox BuildLinkedCheckbox() => new(Text, ScreenAlignment, Offset, Animation, ButtonAction, ButtonInfo, IsTransitionable, IsChecked);

    public ChildSprite BuildChild() => new(Path, Dims, Offset, Parent, IsTransitionable);

    public Clickable BuildClickable() => new(Path, ScreenAlignment, Offset, Dims, Color, HoverColor, 
                UnavailableColor, Animation, HoverScale, IsAvailable, ButtonAction, ButtonInfo, IsTransitionable);

    public Dragable BuildDragable() => new(Path, ScreenAlignment, Offset, Dims, Color, Animation, ButtonInfo, IsTransitionable);

    public SpriteBuilder WithPath(string PATH)
    {
        Path = PATH;
        return this;
    }

    public SpriteBuilder WithText(string TEXT)
    {
        Text = TEXT;
        return this;
    }

    public SpriteBuilder WithFont(SpriteFont FONT)
    {
        Font = FONT;
        return this;
    }

    public SpriteBuilder WithAbsolutePosition(Vector2 POS)
    {
        ScreenAlignment = Alignment.TOP_LEFT;
        Offset = POS;
        return this;
    }

    public SpriteBuilder WithOffset(Vector2 OFFSET)
    {
        Offset = OFFSET;
        return this;
    }

    public SpriteBuilder WithDims(Vector2 DIMS)
    {
        Dims = DIMS;
        return this;
    }

    public SpriteBuilder WithHoverScale(Vector2 HOVERSCALE)
    {
        HoverScale = HOVERSCALE;
        return this;
    }

    public SpriteBuilder WithScreenAlignment(Alignment ALIGNMENT)
    {
        ScreenAlignment = ALIGNMENT;
        return this;
    }

    public SpriteBuilder WithColor(Color COLOR)
    {
        Color = COLOR;
        return this;
    }

    public SpriteBuilder WithHoverColor(Color COLOR)
    {
        HoverColor = COLOR;
        return this;
    }

    public SpriteBuilder WithUnavailableColor(Color COLOR)
    {
        UnavailableColor = COLOR;
        return this;
    }

    public SpriteBuilder WithAnimation(IAnimate ANIMATION)
    {
        Animation = ANIMATION;
        return this;
    } 

    public SpriteBuilder WithButtonAction(EventHandler<object> BUTTONACTION)
    {
        ButtonAction = BUTTONACTION;
        return this;
    }

    public SpriteBuilder WithButtonInfo(object INFO)
    {
        ButtonInfo = INFO;
        return this;
    }

    public SpriteBuilder WithParent(Sprite PARENT)
    {
        Parent = PARENT;
        return this;
    }

    public SpriteBuilder WithTransitionable(bool ISTRANSITIONABLE)
    {
        IsTransitionable = ISTRANSITIONABLE;
        return this;
    }

    public SpriteBuilder WithChecked(bool ISCHECKED)
    {
        IsChecked = ISCHECKED;
        return this;
    }

    public SpriteBuilder WithAvailable(bool ISAVAILABLE)
    {
        IsAvailable = ISAVAILABLE;
        return this;
    }
}