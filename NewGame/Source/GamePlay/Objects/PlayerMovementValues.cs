public struct PlayerMovementValues
{
    public static float horizontalAcceleration = 0.1f;
    public static float horizontalDeceleration = 0.075f;
    public static float maxSpeed = 1.5f;

    public static float dashSpeed = 4;
    public static float dashDeceleration = 0.3f;

    public static float jumpSpeed = 1.5f;
    public static int jumpHoldTime = 350;
    public static int jumpBufferTime = 50;

    public static float gravity = 0.75f;
    public static float maxFallSpeed = 3;

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 0.1f;
        horizontalDeceleration = 0.075f;
        maxSpeed = 1.5f;

        dashSpeed = 4;
        dashDeceleration = 0.3f;
        
        jumpSpeed = 1.5f;
        jumpHoldTime = 350;
        jumpBufferTime = 50;

        gravity = 0.75f;
        maxFallSpeed = 3;
    }
}