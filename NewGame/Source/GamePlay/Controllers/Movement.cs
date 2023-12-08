using Microsoft.Xna.Framework;

public class Movement
{
    private Jump jump;
    
    private float horizontalSpeed;
    private float verticalSpeed;
    private Vector2 velocity = new();
    
    private bool grounded;
    private bool blockedLeft;
    private bool blockedRight;
    private bool blockedTop;

    private bool canDash;
    private MyTimer dashTimer;
    private MyTimer cayoteTimer;

    private static bool IsCat => GameGlobals.currentMode == CharacterMode.CAT;
    private static bool IsFrog => GameGlobals.currentMode == CharacterMode.FROG;
    private static bool IsGecko => GameGlobals.currentMode == CharacterMode.GECKO;

    public Movement()
    {
        jump = new();
        dashTimer = new(PlayerMovementValues.dashTime, true);
        cayoteTimer = new(PlayerMovementValues.jumpBufferTime, true);
        GameGlobals.currentMode = CharacterMode.CAT;
    }

    public void Update(Hitbox SPRITE_BOX)
    {
        CheckForContact(SPRITE_BOX);
        jump.Update();
        dashTimer.UpdateTimer();
        cayoteTimer.UpdateTimer();
    }
    
    public void CheckForContact(Hitbox SPRITE_BOX)
    {
        ResetContacts();

        foreach (Hitbox box in Platforms.hitboxes)
        {
            if (box.IsAbove(SPRITE_BOX)) blockedTop = true;
            if (box.IsLeft(SPRITE_BOX)) 
            {
                blockedLeft = true;
                if (IsGecko) cayoteTimer.ResetToZero();
            }
            if (box.IsRight(SPRITE_BOX)) 
            {
                blockedRight = true;
                if (IsGecko) cayoteTimer.ResetToZero();
            }
            if (box.IsBelow(SPRITE_BOX)) 
            {
                grounded = true;
                cayoteTimer.ResetToZero();
                canDash = true;
                jump.CanDoubleJump = true;
            }
        }
    }

    private void Dash()
    {
        if (InputController.Dash() && IsCat && canDash)
        {
            horizontalSpeed = GameGlobals.facingLeft ? -PlayerMovementValues.dashSpeed : PlayerMovementValues.dashSpeed;
            canDash = false;
            dashTimer.ResetToZero();
        }
    }
    
    private void Fall()
    {
        verticalSpeed = verticalSpeed < PlayerMovementValues.maxFallSpeed ? verticalSpeed + PlayerMovementValues.gravity : PlayerMovementValues.maxFallSpeed;
    }

    private void HorizontalMovement()
    {
        if ((blockedLeft && horizontalSpeed < 0) || (blockedRight && horizontalSpeed > 0)) horizontalSpeed = 0;
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
            if (horizontalSpeed != 0) return CharacterState.RUNNING;
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
        if (!dashTimer.Test()) return;
        if (InputController.Left() && !InputController.Right())
        {
            if (horizontalSpeed > -PlayerMovementValues.maxSpeed)
            {
                horizontalSpeed -= PlayerMovementValues.horizontalAcceleration * 2;
            }
            else if (horizontalSpeed < -PlayerMovementValues.maxSpeed)
            {
                horizontalSpeed += PlayerMovementValues.horizontalDeceleration * 2;
                
                if (horizontalSpeed > -PlayerMovementValues.maxSpeed)
                    horizontalSpeed = -PlayerMovementValues.maxSpeed;
            }
        } else if (InputController.Right() && !InputController.Left())
        {
            if (horizontalSpeed < PlayerMovementValues.maxSpeed)
            {
                horizontalSpeed += PlayerMovementValues.horizontalAcceleration;
            }
            else if (horizontalSpeed > PlayerMovementValues.maxSpeed)
            {
                horizontalSpeed -= PlayerMovementValues.horizontalDeceleration;

                if (horizontalSpeed < PlayerMovementValues.maxSpeed)
                    horizontalSpeed = PlayerMovementValues.maxSpeed;
            }
        } else if (horizontalSpeed > 0)
        {
            horizontalSpeed -= horizontalSpeed > PlayerMovementValues.maxSpeed ? PlayerMovementValues.dashDeceleration : PlayerMovementValues.horizontalDeceleration;
            if (horizontalSpeed < 0) horizontalSpeed = 0;
        } else {
            horizontalSpeed += horizontalSpeed < -PlayerMovementValues.maxSpeed ? PlayerMovementValues.dashDeceleration : PlayerMovementValues.horizontalDeceleration;
            if (horizontalSpeed > 0) horizontalSpeed = 0;
        }
    }

    private void SetVelocity()
    {
        if (!dashTimer.Test())
        {
            velocity = new Vector2(horizontalSpeed, 0) * Globals.gameTime.ElapsedGameTime.Milliseconds;
            return;
        }
        if (!cayoteTimer.Test() || jump.IsJumping || (IsGecko && (blockedLeft || blockedRight)))
        {
            verticalSpeed = jump.GetFallSpeed();
        } else if (jump.CanDoubleJump && IsFrog && InputController.DoubleJump())
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
                verticalSpeed = blockedTop ? 0 : -horizontalSpeed * PlayerMovementValues.climbSpeedRatio;
                velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
                return true;
            }
            if (horizontalSpeed <= 0 && blockedLeft)
            {
                verticalSpeed = blockedTop ? 0 : horizontalSpeed * PlayerMovementValues.climbSpeedRatio;
                velocity = new Vector2(0, verticalSpeed) * Globals.gameTime.ElapsedGameTime.Milliseconds;
                return true;
            }
        }
        return false;
    }
}