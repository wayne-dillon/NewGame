public struct PlayerMovementValues
{
    public static float horizontalAcceleration = 0.06f;
    public static float horizontalDeceleration = 0.05f;
    public static float maxSpeed = 0.9f;

    public static float climbSpeedRatio = 2f/3f;

    public static float dashSpeed = 2;
    public static int dashTime = 220;
    public static float dashDeceleration = 0.5f;

    public static float jumpSpeed = 0.95f;
    public static int jumpHoldTime = 250;
    public static int jumpBufferTime = 50;

    public static float gravity = 0.2f;
    public static float maxFallSpeed = 1.0f;

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 0.06f;
        horizontalDeceleration = 0.05f;
        maxSpeed = 0.9f;

        dashSpeed = 2;
        dashTime = 220;
        dashDeceleration = 0.5f;
        
        jumpSpeed = 0.95f;
        jumpHoldTime = 250;
        jumpBufferTime = 50;

        gravity = 0.2f;
        maxFallSpeed = 1.0f;
    }
}