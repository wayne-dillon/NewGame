public struct PlayerMovementValues
{
    public static float horizontalAcceleration = 0.1f;
    public static float horizontalDeceleration = 0.9f;
    public static float maxSpeed = 0.6f;

    public static float climbSpeedRatio = 2f/3f;

    public static float dashSpeed = 2;
    public static int dashTime = 220;
    public static float dashDeceleration = 0.5f;

    public static float jumpSpeed = 0.9f;
    public static int jumpHoldTime = 250;
    public static int jumpBufferTime = 50;

    public static float gravity = 0.06f;
    public static float maxFallSpeed = 0.9f;

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 0.06f;
        horizontalDeceleration = 0.05f;
        maxSpeed = 0.6f;

        dashSpeed = 2;
        dashTime = 220;
        dashDeceleration = 0.5f;
        
        jumpSpeed = 0.9f;
        jumpHoldTime = 250;
        jumpBufferTime = 50;

        gravity = 0.06f;
        maxFallSpeed = 0.9f;
    }
}