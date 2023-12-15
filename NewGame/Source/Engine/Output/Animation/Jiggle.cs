using Microsoft.Xna.Framework;

public class Jiggle : IAnimate
{
    private Vector2 maxDims;
    private float rotSpeed;
    private float shrinkSpeed;
    private int changePerc;
    private bool shrinking;

    public Jiggle(Vector2 MAXDIMS, float ROTSPEED, float SHRINKSPEED, int CHANGEPERC)
    {
        maxDims = MAXDIMS;
        rotSpeed = ROTSPEED;
        shrinkSpeed = SHRINKSPEED;
        changePerc = CHANGEPERC;
        shrinking = true;
    }

    public void Update()
    {
    }

    public void Animate(Animatable TARGET)
    {
        TARGET.rot += rotSpeed * Globals.gameTime.ElapsedGameTime.Milliseconds;
        if (TARGET.rot > 360)
            TARGET.rot -= 360;

        float sizePerc = TARGET.dims.X / maxDims.X;
        float change = shrinkSpeed * Globals.gameTime.ElapsedGameTime.Milliseconds;
        if (sizePerc + change > 1)
        {
            shrinking = true;
        } else if (sizePerc - change < 0.5f) {
            shrinking = false;
        } else {
            int rand = Globals.random.Next(1, 100);
            if (rand < changePerc)
            {
                shrinking = !shrinking;
            }
        }

        float newSizePerc = shrinking ? sizePerc - change : sizePerc + change;

        TARGET.dims = maxDims * newSizePerc;
    }

    public bool IsComplete() => false;
}