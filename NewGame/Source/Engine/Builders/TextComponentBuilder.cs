using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextComponentBuilder
{
    private string Text = "";
    private SpriteFont Font;
    private Vector2 Offset;
    private Alignment ScreenAlignment = Alignment.CENTER;
    private Alignment TextAlignment = Alignment.CENTER;
    private Color Color = Color.Black;
    private IAnimate Animation;
    private bool IsTransitionable = true;
    private bool IsUI = true;

    public TextComponent Build() => new(Text, Font, TextAlignment, ScreenAlignment, Offset, Color, Animation, IsTransitionable, IsUI);

    public TextComponentBuilder WithText(string TEXT)
    {
        Text = TEXT;
        return this;
    }

    public TextComponentBuilder WithFont(SpriteFont FONT)
    {
        Font = FONT;
        return this;
    }

    public TextComponentBuilder WithAbsolutePosition(Vector2 POS)
    {
        ScreenAlignment = Alignment.TOP_LEFT;
        Offset = POS;
        return this;
    }

    public TextComponentBuilder WithOffset(Vector2 OFFSET)
    {
        Offset = OFFSET;
        return this;
    }

    public TextComponentBuilder WithScreenAlignment(Alignment ALIGNMENT)
    {
        ScreenAlignment = ALIGNMENT;
        return this;
    }

    public TextComponentBuilder WithTextAlignment(Alignment ALIGNMENT)
    {
        TextAlignment = ALIGNMENT;
        return this;
    }

    public TextComponentBuilder WithColor(Color COLOR)
    {
        Color = COLOR;
        return this;
    }

    public TextComponentBuilder WithAnimation(IAnimate ANIMATION)
    {
        Animation = ANIMATION;
        return this;
    }

    public TextComponentBuilder WithTransitionable(bool ISTRANSITIONABLE)
    {
        IsTransitionable = ISTRANSITIONABLE;
        return this;
    }

    public TextComponentBuilder WithUI(bool ISUI)
    {
        IsUI = ISUI;
        return this;
    }
}