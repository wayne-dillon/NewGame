using Microsoft.Xna.Framework;

public abstract class Mode
{
    protected readonly Player player;

    protected readonly float horizontalAcceleration;
    protected readonly float horizontalDeceleration;
    protected readonly float maxSpeed;

    protected readonly float gravity;
    protected readonly float maxFallSpeed;

    public Mode(Player PLAYER, float HORIZONTAL_ACC, float HORIZONTAL_DEC, float MAXSPEED, float GRAVITY, float MAXFALLSPEED)
    {
        player = PLAYER;
        horizontalAcceleration = HORIZONTAL_ACC;
        horizontalDeceleration = HORIZONTAL_DEC;
        maxSpeed = MAXSPEED;
        gravity = GRAVITY;
        maxFallSpeed = MAXFALLSPEED;
    }

    public abstract void MovementControl();
    
    protected void SetSpeed()
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

    protected void MoveLeft()
    {
        player.velocity = new Vector2(player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
    }

    protected void MoveRight()
    {
        player.velocity = new Vector2(player.speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
    }

    protected void Fall()
    {
        player.fallSpeed = player.fallSpeed < maxFallSpeed ? player.fallSpeed + gravity : maxFallSpeed;
        player.velocity = new Vector2(player.speed, player.fallSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
    }
}