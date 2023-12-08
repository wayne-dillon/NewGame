using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextComponent : Animatable
{
    private string text;
    private SpriteFont font = Globals.defaultFont;
    private bool variableFontSize;
    private Vector2 absolutePos;
    private readonly Alignment textAlignment;

    public TextComponent(string TEXT, SpriteFont FONT, Alignment TEXTALIGNMENT, Alignment SCREENALIGNMENT, Vector2 OFFSET, Color COLOR, IAnimate ANIMATION, bool ISTRANSITIONABLE, bool ISUI)
        : base(COLOR, Coordinates.Get(SCREENALIGNMENT) + OFFSET, Vector2.Zero, SCREENALIGNMENT, ANIMATION, InteractableType.NONE, ISTRANSITIONABLE, ISUI)
    {
        text = TEXT;
        if (FONT == null)
        {
            variableFontSize = true;
        } else {
            font = FONT;
        }
        textAlignment = TEXTALIGNMENT;

        SetAnchor(Pos);
    }

    public override void Update() 
    {
        if (variableFontSize && font != Globals.defaultFont)
        {
            font = Globals.defaultFont;
        }
        base.Update();
    }

    public void Update(string TEXT) 
    {
        text = TEXT;
        if (variableFontSize && font != Globals.defaultFont)
        {
            font = Globals.defaultFont;
        }
        absolutePos = Pos + GetTextAlignmentOffset();
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
        Pos = ANCHOR;
        absolutePos = Pos + GetTextAlignmentOffset();
    }

    private void SetPosition(Vector2 OFFSET)
    {
        Pos = Coordinates.Get(alignment) + OFFSET;
        absolutePos = Pos + GetTextAlignmentOffset();
    }

    public void Draw() 
    {
        Vector2 POS = absolutePos;
        if (Globals.gameState == GameState.GAME_PLAY && !isUI)
        {
            POS -= Globals.screenPosition;
        }

        Globals.spriteBatch.DrawString(font, text, POS * Globals.ScalingFactor(), color);
    }

    public void Draw(string TEXT, Vector2 POS, Color COLOR) 
    {
        if (Globals.gameState == GameState.GAME_PLAY && !isUI)
        {
            POS -= Globals.screenPosition;
        }

        text = TEXT;
        SetPosition(POS); 
        Globals.spriteBatch.DrawString(font, text, absolutePos * Globals.ScalingFactor(), new Color(COLOR, color.A));
    }
}