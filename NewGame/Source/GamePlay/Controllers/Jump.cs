public class Jump
{
    private PlayerMovementValues values;
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
        get { return canDoubleJump; }
    }

    public Jump(PlayerMovementValues VALUES)
    {
        values = VALUES;
        inputTimer = new(values.jumpHoldTime);
        bufferTimer = new(values.jumpBufferTime);
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
                canDoubleJump = true;
                inputTimer.ResetToZero();
            } else if (inputTimer.Test()) {
                isJumping = false;
                return 0;
            }
            return -values.jumpSpeed;
        }
        isJumping = false;
        return 0;
    }

    public float GetDoubleJump()
    {
        if (InputController.DoubleJump())
        {
            canDoubleJump = false;
            isJumping = true;
            inputTimer.ResetToZero();
        }
        return GetFallSpeed();
    }
}