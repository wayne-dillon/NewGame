using Microsoft.Xna.Framework;

public class ScrollDiagonal : IAnimate
{
    private float speed;

    public ScrollDiagonal(float SPEED)
    {
        speed = SPEED;
    }

    public void Update()
    {
    }

    public void Animate(Animatable TARGET)
    {
        float shift = Globals.gameTime.ElapsedGameTime.Milliseconds * speed;
        if (TARGET.Pos.Y >= 1000)
        {
            TARGET.Pos -= new Vector2(1100, 1100);
        } else {
            TARGET.Pos += new Vector2(shift, shift);
        }
    }

    public bool IsComplete() => false;
}