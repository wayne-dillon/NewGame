public struct PlayerMovementValues
{
    public static float horizontalAcceleration = 0.06f;
    public static float horizontalDeceleration = 0.05f;
    public static float maxSpeed = 0.9f;

    public static float dashSpeed = 3;
    public static int dashTime = 150;
    public static float dashDeceleration = 0.5f;

    public static float jumpSpeed = 1.5f;
    public static int jumpHoldTime = 250;
    public static int jumpBufferTime = 50;

    public static float gravity = 0.3f;
    public static float maxFallSpeed = 1.5f;

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 0.06f;
        horizontalDeceleration = 0.05f;
        maxSpeed = 0.9f;

        dashSpeed = 3;
        dashTime = 150;
        dashDeceleration = 0.5f;
        
        jumpSpeed = 1.5f;
        jumpHoldTime = 250;
        jumpBufferTime = 50;

        gravity = 0.3f;
        maxFallSpeed = 1.5f;
    }
}