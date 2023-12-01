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
    private CharacterMode currentMode;

    public Movement()
    {
        values = new(0.3f, 0.25f, 1.5f, 2, 100, 100, 0.2f, 4);
        currentMode = CharacterMode.CAT;
    }
    
    public void CheckForContact(Hitbox spriteBox)
    {
        ResetContacts();

        foreach (Hitbox box in Platforms.hitboxes)
        {
            switch (spriteBox.GetContactDirection(box))
            {
                case Direction.NONE:
                case Direction.UP_LEFT:
                case Direction.UP_RIGHT:
                    break;
                case Direction.LEFT:
                    blockedLeft = true;
                    fallSpeed = 0;
                    break;
                case Direction.RIGHT:
                    blockedRight = true;
                    fallSpeed = 0;
                    break;
                case Direction.DOWN:
                case Direction.DOWN_LEFT:
                case Direction.DOWN_RIGHT:
                    grounded = true;
                    fallSpeed = 0;
                    break;
            }
        }
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

    public CharacterState GetState()
    {
        if (grounded && speed == 0) return CharacterState.IDLE;
        if (currentMode != CharacterMode.CAT)
        {
            if (currentMode == CharacterMode.GECKO)
            {
                if (blockedLeft && speed < 0) return CharacterState.CLIMBING_LEFT;
                if (blockedRight && speed > 0) return CharacterState.CLIMBING_RIGHT;
            }
            if (blockedLeft) return CharacterState.CLINGING_LEFT;
            if (blockedRight) return CharacterState.CLINGING_RIGHT;
        }
        if (grounded)
        {
            if (speed < 0) return CharacterState.RUNNING_LEFT;
            if (speed > 0) return CharacterState.RUNNING_RIGHT;
        }
        if (velocity.Y < 0) return CharacterState.JUMPING;
        return CharacterState.FALLING;
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

    public void ResetContacts()
    {
        grounded = false;
        blockedLeft = false;
        blockedRight = false;
        blockedTop = false;
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