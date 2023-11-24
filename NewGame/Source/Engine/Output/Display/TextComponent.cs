using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextComponent : Animatable
{
    private string text;
    private readonly SpriteFont font;
    private Vector2 absolutePos;
    private readonly Alignment textAlignment;

    public TextComponent(string TEXT, SpriteFont FONT, Alignment TEXTALIGNMENT, Alignment SCREENALIGNMENT, Vector2 OFFSET, Color COLOR, IAnimate ANIMATION, bool ISTRANSITIONABLE)
        : base(COLOR, Coordinates.Get(SCREENALIGNMENT) + OFFSET, Vector2.Zero, SCREENALIGNMENT, ANIMATION, ISTRANSITIONABLE)
    {
        text = TEXT;
        font = FONT ?? Fonts.defaultFont;
        textAlignment = TEXTALIGNMENT;

        SetAnchor(pos);
    }

    public override void Update() 
    {
        base.Update();
    }

    public void Update(string TEXT) 
    {
        text = TEXT;
        absolutePos = pos + GetTextAlignmentOffset();
        base.Update();
    }

    public Vector2 GetTextAlignmentOffset() 
    {
        return textAlignment switch
        {
            Alignment.CENTER or Alignment.TOP or Alignment.BOTTOM => new Vector2(-font.MeasureString(text).X / 2 / Globals.ScalingFactor().X, -font.MeasureString(text).Y / 2),
            Alignment.CENTER_LEFT or Alignment.TOP_LEFT or Alignment.BOTTOM_LEFT => new Vector2(0, -font.MeasureString(text).Y / 2),
            _ => new Vector2(-font.MeasureString(text).X / Globals.ScalingFactor().X, -font.MeasureString(text).Y / 2),
        };
    }

    public float GetWidth() => font.MeasureString(text).X;

    public void SetAnchor(Vector2 ANCHOR)
    {
        pos = ANCHOR;
        absolutePos = pos + GetTextAlignmentOffset();
    }

    private void SetPosition(Vector2 OFFSET)
    {
        pos = Coordinates.Get(alignment) + OFFSET;
        absolutePos = pos + GetTextAlignmentOffset();
    }

    public void Draw() 
    {
        Globals.spriteBatch.DrawString(font, text, absolutePos * Globals.ScalingFactor(), color);
    }

    public void Draw(string TEXT, Vector2 POS, Color COLOR) 
    {
        text = TEXT;
        SetPosition(POS); 
        Globals.spriteBatch.DrawString(font, text, absolutePos * Globals.ScalingFactor(), new Color(COLOR, color.A));
    }
}