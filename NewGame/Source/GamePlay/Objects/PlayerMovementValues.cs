public struct PlayerMovementValues
{
    public float horizontalAcceleration;
    public float horizontalDeceleration;
    public float maxSpeed;

    public float jumpSpeed;
    public int jumpHoldTime;
    public int jumpBufferTime;

    public float gravity;
    public float maxFallSpeed;

    public PlayerMovementValues(float H_ACC, float H_DEC, float MAX_SPEED, float JUMP_SPEED, int JUMP_HOLD_TIME, int JUMP_BUFFER_TIME,
                                float GRAVITY, float MAX_FALL_SPEED)
    {
        horizontalAcceleration = H_ACC;
        horizontalDeceleration = H_DEC;
        maxSpeed = MAX_SPEED;

        jumpSpeed = JUMP_SPEED;
        jumpHoldTime = JUMP_HOLD_TIME;
        jumpBufferTime = JUMP_BUFFER_TIME;

        gravity = GRAVITY;
        maxFallSpeed = MAX_FALL_SPEED;
    }
}