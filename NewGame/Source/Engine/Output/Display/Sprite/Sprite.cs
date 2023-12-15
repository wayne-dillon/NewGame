using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite : Animatable
{
    public Vector2 screenPos;
    public bool hFlipped;
    public Texture2D myModel;
    public Sprite(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, IAnimate ANIMATION, InteractableType TYPE, bool ISTRANSITIONABLE, bool ISUI)
        : base(COLOR, Coordinates.Get(ALIGNMENT) + OFFSET, DIMS, ALIGNMENT, ANIMATION, TYPE, ISTRANSITIONABLE, ISUI)
    {
        myModel = Globals.content.Load<Texture2D>(PATH);
        screenPos = Pos;
    }

    public override void Update()
    {
        base.Update();
        if ((Globals.gameState == GameState.GAME_PLAY || Globals.gameState == GameState.LEVEL_EDITOR) && !isUI)
        {
            screenPos = Pos - Globals.screenPosition;
        } else {
            screenPos = Pos;
        }
    }

    public virtual bool Hover() {
        return HoverImg();
    }

    public virtual bool HoverImg() { 
        Vector2 mousePos = Globals.mouse.newMousePos / Globals.ScalingFactor();

        if (mousePos.X >= screenPos.X - dims.X / 2 && mousePos.X <= screenPos.X + dims.X / 2
                && mousePos.Y >= screenPos.Y - dims.Y / 2 && mousePos.Y <= screenPos.Y  + dims.Y / 2)
        {
            return true;
        }

        return false;
    }

    public bool IsOnScreen()
    {
        return screenPos.X + myModel.Bounds.Width > 0 && Pos.X - myModel.Bounds.Width < Coordinates.screenWidth &&
            screenPos.Y + myModel.Bounds.Height > 0 && Pos.Y - myModel.Bounds.Height < Coordinates.screenHeight;
    }

    public virtual void Draw() {
        Draw(screenPos, color);
    }

    public virtual void Draw(Color COLOR)
    {
        Draw(screenPos, new Color(COLOR, color.A));
    }

    public virtual void Draw(Vector2 POS)
    {
        Draw(POS, color);
    }

    public virtual void Draw(Vector2 POS, Color COLOR)
    {
        if (myModel != null)
        {
            Globals.spriteBatch.Draw(myModel, new Rectangle((int)(POS.X * Globals.ScalingFactor().X), (int)(POS.Y * Globals.ScalingFactor().Y), 
                (int)(dims.X * Globals.ScalingFactor().X), (int)(dims.Y * Globals.ScalingFactor().Y)), null, COLOR, rot, 
                new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), hFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }
    }
}
