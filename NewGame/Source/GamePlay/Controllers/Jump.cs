public class Jump
{
    private PlayerMovementValues values;
    private MyTimer inputTimer;
    private MyTimer bufferTimer;
    private bool jumpRequested;
    private bool isJumping;

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
            jumpRequested = true;
            bufferTimer.ResetToZero();
        }
    }

    public float GetFallSpeed()
    {
        if (InputController.Jump() || (!isJumping && jumpRequested && !bufferTimer.Test()))
        {
            if (!isJumping)
            {
                isJumping = true;
                jumpRequested = false;
                inputTimer.ResetToZero();
            } else if (inputTimer.Test()) {
                isJumping = false;
                jumpRequested = true;
                bufferTimer.ResetToZero();
                return 0;
            }
            return -values.jumpSpeed;
        }
        isJumping = false;
        return 0;
    }

    public bool IsJumping()
    {
        return isJumping;
    }
}