public class Jump
{
    private MyTimer inputTimer;
    private MyTimer bufferTimer;
    private bool isJumping;
    public bool IsJumping
    {
        get { return isJumping; }
    }

    private bool canDoubleJump;
    public bool CanDoubleJump
    {
        get { return canDoubleJump; } set { canDoubleJump = value; }
    }

    public Jump()
    {
        inputTimer = new(PlayerMovementValues.jumpHoldTime);
        bufferTimer = new(PlayerMovementValues.jumpBufferTime);
    }

    public void Update()
    {
        inputTimer.UpdateTimer();
        bufferTimer.UpdateTimer();
        if (InputController.Jump())
        {
            bufferTimer.ResetToZero();
        }
    }

    public float GetFallSpeed()
    {
        if (InputController.Jump() || (!isJumping && !bufferTimer.Test()))
        {
            if (!isJumping)
            {
                isJumping = true;
                inputTimer.ResetToZero();
            } else if (inputTimer.Test()) {
                isJumping = false;
                return 0;
            }
            return -PlayerMovementValues.JumpSpeed;
        }
        isJumping = false;
        return 0;
    }

    public float GetDoubleJump()
    {
        canDoubleJump = false;
        isJumping = true;
        inputTimer.ResetToZero();
        return GetFallSpeed();
    }
}