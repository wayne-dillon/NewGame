using Microsoft.Xna.Framework;

public class FrogMode : Mode
{
    public FrogMode(Player PLAYER) : base(PLAYER, 0.2f, 0.1f, 0.8f, 0.2f, 4)
    {}

    public override void MovementControl()
    {
        SetSpeed();
        SetVelocity();
    }

    private void SetVelocity()
    {
        if (!player.grounded && !player.blockedLeft && !player.blockedRight)
        {
            Fall();
            return;
        }
        if (player.speed > 0)
        {
            if (player.blockedRight)
            {
                player.velocity = Vector2.Zero;
            } else {
                MoveRight();
            }
        }
        if (player.speed <= 0)
        {
            if (player.blockedLeft)
            {
                player.velocity = Vector2.Zero;
            } else {
                MoveLeft();
            }
        }
    }
}