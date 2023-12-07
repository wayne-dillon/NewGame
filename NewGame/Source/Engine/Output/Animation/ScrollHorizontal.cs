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
        if (speed > 0 && TARGET.Pos.X >= Coordinates.screenWidth)
        {
            TARGET.Pos -= new Vector2(Coordinates.screenWidth * 3, 0);
        } else if (speed < 0 && TARGET.Pos.X <= -Coordinates.screenWidth) {
            TARGET.Pos += new Vector2(Coordinates.screenWidth * 3, 0);
        } else {
            TARGET.Pos += new Vector2(shift, 0);
        }
    }

    public bool IsComplete() => false;
}