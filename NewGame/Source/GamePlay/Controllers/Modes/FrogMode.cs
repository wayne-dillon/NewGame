using Microsoft.Xna.Framework;

public class FrogMode : Mode
{
    public FrogMode(Player PLAYER) : base(PLAYER, 0.2f, 0.1f, 0.8f, 5, 200, 0.2f, 4)
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
        if (InputController.Jump())
        {
            Jump();
            Fall();
            return;
        }
        if (player.speed > 0)
        {
            if (player.blockedRight)
            {
                player.velocity = Vector2.Zero;
            } else {
                HorizontalMovement();
            }
        }
        if (player.speed <= 0)
        {
            if (player.blockedLeft)
            {
                player.velocity = Vector2.Zero;
            } else {
                HorizontalMovement();
            }
        }
    }
}