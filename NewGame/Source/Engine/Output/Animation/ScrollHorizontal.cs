using Microsoft.Xna.Framework;

public class ScrollHorizontal : IAnimate
{
    private float speed;

    public ScrollHorizontal(float SPEED)
    {
        speed = SPEED;
    }

    public void Update()
    {
    }

    public void Animate(Animatable TARGET)
    {
        float shift = Globals.gameTime.ElapsedGameTime.Milliseconds * speed;
        if (TARGET.Pos.X >= Coordinates.screenWidth)
        {
            TARGET.Pos -= new Vector2(Coordinates.screenWidth * 2, 0);
        } else {
            TARGET.Pos += new Vector2(shift, 0);
        }
    }

    public bool IsComplete() => false;
}