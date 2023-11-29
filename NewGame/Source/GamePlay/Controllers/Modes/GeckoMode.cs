using Microsoft.Xna.Framework;

public class GeckoMode : Mode
{
    public GeckoMode(Player PLAYER) : base(PLAYER, 0.3f, 0.25f, 1.5f, 2, 100, 0.2f, 4)
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
                player.velocity = player.blockedTop ? Vector2.Zero : new Vector2(0, -player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else
            {
                HorizontalMovement();
            }
        }
        if (player.speed <= 0)
        {
            if (player.blockedLeft)
            {
                player.velocity = player.blockedTop ? Vector2.Zero : new Vector2(0, player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else
            {
                HorizontalMovement();
            }
        }
    }
}