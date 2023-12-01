using Microsoft.Xna.Framework;

public class Movement
{
    PlayerMovementValues values;
    
    private float speed;
    private float fallSpeed;
    private Vector2 velocity = new();
    private bool grounded;
    private bool blockedLeft;
    private bool blockedRight;
    private bool blockedTop;

    public Movement()
    {
        values = new()
    }
    
    private void Fall()
    {
        fallSpeed = fallSpeed < values.maxFallSpeed ? fallSpeed + values.gravity : values.maxFallSpeed;
        velocity = new Vector2(speed, fallSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
    }

    private void HorizontalMovement()
    {
        velocity = new Vector2(speed * Globals.gameTime.ElapsedGameTime.Milliseconds, 0);
    }

    public Vector2 GetVelocity()
    {
        SetSpeed();
        SetVelocity();
        return velocity;
    }

    private void Jump()
    {
        fallSpeed = -values.jumpSpeed;
    }

    private void SetSpeed()
    {
        if (InputController.Left() && !InputController.Right())
        {
            speed -= values.horizontalAcceleration;
            if (speed < -values.maxSpeed)
                speed = -values.maxSpeed;
        } else if (InputController.Right() && !InputController.Left())
        {
            speed += values.horizontalAcceleration;
            if (speed > values.maxSpeed)
                speed = values.maxSpeed;
        } else if (speed > 0)
        {
            speed -= values.horizontalDeceleration;
            if (speed < 0) speed = 0;
        } else {
            speed += values.horizontalDeceleration;
            if (speed > 0) speed = 0;
        }
    }

    private void SetVelocity()
    {
        if (!grounded && !blockedLeft && !blockedRight)
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
        if (speed > 0)
        {
            if (blockedRight)
            {
                velocity = blockedTop ? Vector2.Zero : new Vector2(0, -speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else
            {
                HorizontalMovement();
            }
        }
        if (speed <= 0)
        {
            if (blockedLeft)
            {
                velocity = blockedTop ? Vector2.Zero : new Vector2(0, speed * Globals.gameTime.ElapsedGameTime.Milliseconds);
            } else
            {
                HorizontalMovement();
            }
        }
    }
}