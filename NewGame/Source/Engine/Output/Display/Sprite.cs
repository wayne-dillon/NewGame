using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Sprite : Animatable
{
    public float rot;
    public Texture2D myModel;
    public Sprite(string PATH, Alignment ALIGNMENT, Vector2 OFFSET, Vector2 DIMS, Color COLOR, IAnimate ANIMATION, bool ISTRANSITIONABLE)
        : base(COLOR, Coordinates.Get(ALIGNMENT) + OFFSET, DIMS, ALIGNMENT, ANIMATION, ISTRANSITIONABLE)
    {
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

        if (mousePos.X >= pos.X - dims.X / 2 && mousePos.X <= pos.X + dims.X / 2
                && mousePos.Y >= pos.Y - dims.Y / 2 && mousePos.Y <= pos.Y  + dims.Y / 2)
        {
            return true;
        }

        return false;
    }

    public bool IsOnScreen()
    {
        return pos.X + myModel.Bounds.Width > 0 && pos.X - myModel.Bounds.Width < Coordinates.screenWidth &&
            pos.Y + myModel.Bounds.Height > 0 && pos.Y - myModel.Bounds.Height < Coordinates.screenHeight;
    }

    public virtual void Draw() {
        Draw(pos, color);
    }

    public virtual void Draw(Color COLOR)
    {
        Draw(pos, new Color(COLOR, color.A));
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
                new Vector2(myModel.Bounds.Width / 2, myModel.Bounds.Height / 2), new SpriteEffects(), 0);
        }
    }
}
