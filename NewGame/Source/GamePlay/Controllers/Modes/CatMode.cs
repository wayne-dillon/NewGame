using Microsoft.Xna.Framework;

public class CatMode : Mode
{
    public CatMode(Player PLAYER) : base(PLAYER, 0.5f, 0.3f, 3, 3, 150, 0.2f, 4)
    {}

    public override void MovementControl()
    {
        SetSpeed();
        SetVelocity();
    }

    private void SetVelocity()
    {
        if (!player.grounded)
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