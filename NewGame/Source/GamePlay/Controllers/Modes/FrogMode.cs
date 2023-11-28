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

    private void SetSpeed()
    {
        if (InputController.Left() && !InputController.Right())
        {
            player.speed -= horizontalAcceleration;
            if (player.speed < -maxSpeed)
                player.speed = -maxSpeed;
        } else if (InputController.Right() && !InputController.Left())
        {
            player.speed += horizontalAcceleration;
            if (player.speed > maxSpeed)
                player.speed = maxSpeed;
        } else if (player.speed > 0)
        {
            player.speed -= horizontalDeceleration;
            if (player.speed < 0) player.speed = 0;
        } else {
            player.speed += horizontalDeceleration;
            if (player.speed > 0) player.speed = 0;
        }
    }

    private void SetVelocity()
    {
        if (!player.grounded && !player.blockedLeft && !player.blockedRight)
        {
            player.fallSpeed = player.fallSpeed < maxFallSpeed ? player.fallSpeed + gravity : maxFallSpeed;
            player.velocity = new Vector2(player.speed, player.fallSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
            return;
        }
        if (player.speed > 0)
        {
            if (player.blockedRight)
            {
                player.velocity = player.blockedTop ? Vector2.Zero : new Vector2(0, -player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else {
                player.velocity = new Vector2(player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            }
        }
        if (player.speed <= 0)
        {
            if (player.blockedLeft)
            {
                player.velocity = player.blockedTop ? Vector2.Zero : new Vector2(0, player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else {
                player.velocity = new Vector2(player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
            }
        }
    }

}