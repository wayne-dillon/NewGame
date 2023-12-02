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

    private bool canDash;

    private static bool IsCat => GameGlobals.currentMode == CharacterMode.CAT;
    private static bool IsFrog => GameGlobals.currentMode == CharacterMode.FROG;
    private static bool IsGecko => GameGlobals.currentMode == CharacterMode.GECKO;

    public Movement()
    {
        values = new(0.1f, 0.075f, 1.5f, 4, 0.3f, 1.5f, 350, 50, 0.075f, 3);
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
                    canDash = true;
                    break;
            }
        }
    }

    private void Dash()
    {
        if (InputController.Dash() && IsCat && canDash)
        {
            horizontalSpeed += InputController.Left() || horizontalSpeed < 0 ? -values.dashSpeed : values.dashSpeed;
            canDash = false;
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
        if (IsGecko)
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
        SetHorizontalSpeed();
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

    private void SetHorizontalSpeed()
    {
        if (InputController.Left() && !InputController.Right())
        {
            if (horizontalSpeed > -values.maxSpeed)
            {
                horizontalSpeed -= values.horizontalAcceleration * 2;
            }
            else if (horizontalSpeed < -values.maxSpeed)
            {
                horizontalSpeed += values.horizontalDeceleration * 2;
                
                if (horizontalSpeed > -values.maxSpeed)
                    horizontalSpeed = -values.maxSpeed;
            }
        } else if (InputController.Right() && !InputController.Left())
        {
            if (horizontalSpeed < values.maxSpeed)
            {
                horizontalSpeed += values.horizontalAcceleration;
            }
            else if (horizontalSpeed > values.maxSpeed)
            {
                horizontalSpeed -= values.horizontalDeceleration;

                if (horizontalSpeed < values.maxSpeed)
                    horizontalSpeed = values.maxSpeed;
            }
        } else if (horizontalSpeed > 0)
        {
            horizontalSpeed -= horizontalSpeed > values.maxSpeed ? values.dashDeceleration : values.horizontalDeceleration;
            if (horizontalSpeed < 0) horizontalSpeed = 0;
        } else {
            horizontalSpeed += horizontalSpeed < -values.maxSpeed ? values.dashDeceleration : values.horizontalDeceleration;
            if (horizontalSpeed > 0) horizontalSpeed = 0;
        }
    }

    private void SetVelocity()
    {
        if (grounded || jump.IsJumping || (IsGecko && (blockedLeft || blockedRight)))
        {
            verticalSpeed = jump.GetFallSpeed();
        } else if (jump.CanDoubleJump && IsFrog)
        {
            verticalSpeed = jump.GetDoubleJump();
        }
        if (WallRun()) return;
        if (!grounded && !jump.IsJumping)
        {
            Fall();
        }
        Dash();
        HorizontalMovement();
    }

    private bool WallRun()
    {
        if (IsGecko)
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