using Microsoft.Xna.Framework;

public class Movement
{
    private PlayerMovementValues values;
    private Jump jump;
    
    private float horizontalSpeed;
    private float verticalSpeed;
    private Vector2 velocity = new();
    
    private bool grounded;
    private bool blockedLeft;
    private bool blockedRight;
    private bool blockedTop;

    private CharacterMode currentMode;

    public Movement()
    {
        values = new(0.3f, 0.25f, 1.5f, 2, 150, 100, 0.2f, 4);
        jump = new(values);
        currentMode = CharacterMode.CAT;
    }

    public void Update(Hitbox SPRITE_BOX)
    {
        CheckForContact(SPRITE_BOX);
        jump.Update();
    }
    
    public void CheckForContact(Hitbox SPRITE_BOX)
    {
        ResetContacts();

        foreach (Hitbox box in Platforms.hitboxes)
        {
            switch (SPRITE_BOX.GetContactDirection(box))
            {
                case Direction.NONE:
                case Direction.UP_LEFT:
                case Direction.UP_RIGHT:
                    break;
                case Direction.UP:
                    blockedTop = true;
                    break;
                case Direction.LEFT:
                    blockedLeft = true;
                    break;
                case Direction.RIGHT:
                    blockedRight = true;
                    break;
                case Direction.DOWN:
                case Direction.DOWN_LEFT:
                case Direction.DOWN_RIGHT:
                    grounded = true;
                    break;
            }
        }
    }
    
    private void Fall()
    {
        verticalSpeed = verticalSpeed < values.maxFallSpeed ? verticalSpeed + values.gravity : values.maxFallSpeed;
    }

    private void HorizontalMovement()
    {
        velocity = new Vector2(horizontalSpeed, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
    }

    public CharacterState GetState()
    {
        if (grounded && horizontalSpeed == 0) return CharacterState.IDLE;
        if (currentMode != CharacterMode.CAT)
        {
            if (currentMode == CharacterMode.GECKO)
            {
                if (blockedLeft && horizontalSpeed < 0) return CharacterState.CLIMBING_LEFT;
                if (blockedRight && horizontalSpeed > 0) return CharacterState.CLIMBING_RIGHT;
            }
            if (blockedLeft) return CharacterState.CLINGING_LEFT;
            if (blockedRight) return CharacterState.CLINGING_RIGHT;
        }
        if (grounded)
        {
            if (horizontalSpeed < 0) return CharacterState.RUNNING_LEFT;
            if (horizontalSpeed > 0) return CharacterState.RUNNING_RIGHT;
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
            horizontalSpeed -= values.horizontalAcceleration;
            if (horizontalSpeed < -values.maxSpeed)
                horizontalSpeed = -values.maxSpeed;
        } else if (InputController.Right() && !InputController.Left())
        {
            horizontalSpeed += values.horizontalAcceleration;
            if (horizontalSpeed > values.maxSpeed)
                horizontalSpeed = values.maxSpeed;
        } else if (horizontalSpeed > 0)
        {
            horizontalSpeed -= values.horizontalDeceleration;
            if (horizontalSpeed < 0) horizontalSpeed = 0;
        } else {
            horizontalSpeed += values.horizontalDeceleration;
            if (horizontalSpeed > 0) horizontalSpeed = 0;
        }
    }

    private void SetVelocity()
    {
        if (grounded || jump.IsJumping())
        {
            verticalSpeed = jump.GetFallSpeed();
        }
        if (!grounded && !blockedLeft && !blockedRight && !jump.IsJumping())
        {
            Fall();
        }
        if (horizontalSpeed > 0 && blockedRight)
        {
            verticalSpeed = blockedTop ? 0 : -horizontalSpeed;
            velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
            return;
        }
        if (horizontalSpeed <= 0 && blockedLeft)
        {
            verticalSpeed = blockedTop ? 0 : horizontalSpeed;
            velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
            return;
        }
        HorizontalMovement();
    }
}