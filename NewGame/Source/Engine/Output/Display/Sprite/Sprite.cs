using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite : Animatable
{
    public float rot;
    public bool ui;
    public bool hFlipped;
    public Texture2D myModel;
    public Sprite(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, IAnimate ANIMATION, InteractableType TYPE, bool ISTRANSITIONABLE, bool ISUI)
        : base(COLOR, Coordinates.Get(ALIGNMENT) + OFFSET, DIMS, ALIGNMENT, ANIMATION, TYPE, ISTRANSITIONABLE)
    {
        ui = ISUI;
        myModel = Globals.content.Load<Texture2D>(PATH);
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual bool Hover() {
        return HoverImg();
    }

    public virtual bool HoverImg() { 
        Vector2 mousePos = Globals.mouse.newMousePos;

        if (mousePos.X >= Pos.X - dims.X / 2 && mousePos.X <= Pos.X + dims.X / 2
                && mousePos.Y >= Pos.Y - dims.Y / 2 && mousePos.Y <= Pos.Y  + dims.Y / 2)
        {
            return true;
        }

        return false;
    }

    public bool IsOnScreen()
    {
        return Pos.X + myModel.Bounds.Width > 0 && Pos.X - myModel.Bounds.Width < Coordinates.screenWidth &&
            Pos.Y + myModel.Bounds.Height > 0 && Pos.Y - myModel.Bounds.Height < Coordinates.screenHeight;
    }

    public virtual void Draw() {
        Draw(Pos, color);
    }

    public virtual void Draw(Color COLOR)
    {
        Draw(Pos, new Color(COLOR, color.A));
    }

    public virtual void Draw(Vector2 POS)
    {
        Draw(POS, color);
    }

    public virtual void Draw(Vector2 POS, Color COLOR)
    {
        if (Globals.gameState == GameState.GAME_PLAY && !ui)
        {
            POS -= Globals.screenPosition;
        }

        if (myModel != null)
        {
            Globals.spriteBatch.Draw(myModel, new Rectangle((int)(POS.X * Globals.ScalingFactor().X), (int)(POS.Y * Globals.ScalingFactor().Y), 
                (int)(dims.X * Globals.ScalingFactor().X), (int)(dims.Y * Globals.ScalingFactor().Y)), null, COLOR, rot, 
                new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), hFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }
    }
}
