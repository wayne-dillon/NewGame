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

    private bool isCat => GameGlobals.currentMode == CharacterMode.CAT;
    private bool isFrog => GameGlobals.currentMode == CharacterMode.FROG;
    private bool isGecko => GameGlobals.currentMode == CharacterMode.GECKO;

    public Movement()
    {
        values = new(0.3f, 0.25f, 1.5f, 1.5f, 350, 50, 0.175f, 3);
        jump = new(values);
        GameGlobals.currentMode = CharacterMode.CAT;
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
                case Direction.DOWN_LEFT:
                case Direction.DOWN_RIGHT:
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
        if (isGecko)
        {
            if (blockedLeft && horizontalSpeed < 0) return CharacterState.CLIMBING_LEFT;
            if (blockedRight && horizontalSpeed > 0) return CharacterState.CLIMBING_RIGHT;
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
        if (grounded || jump.IsJumping || (isGecko && (blockedLeft || blockedRight)))
        {
            verticalSpeed = jump.GetFallSpeed();
        } else if (jump.CanDoubleJump && isFrog)
        {
            verticalSpeed = jump.GetDoubleJump();
        }
        if (WallRun()) return;
        if (!grounded && !jump.IsJumping)
        {
            Fall();
        }
        HorizontalMovement();
    }

    private bool WallRun()
    {
        if (isGecko)
        {
            if (horizontalSpeed >= 0 && blockedRight)
            {
                verticalSpeed = blockedTop ? 0 : -horizontalSpeed;
                velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
                return true;
            }
            if (horizontalSpeed <= 0 && blockedLeft)
            {
                verticalSpeed = blockedTop ? 0 : horizontalSpeed;
                velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
                return true;
            }
        }
        return false;
    }
}