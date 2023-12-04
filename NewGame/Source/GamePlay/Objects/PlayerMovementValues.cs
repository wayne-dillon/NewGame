public struct PlayerMovementValues
{
    public static float horizontalAcceleration = 0.09f;
    public static float horizontalDeceleration = 0.08f;
    public static float maxSpeed = 1.2f;

    public static float dashSpeed = 3;
    public static int dashTime = 150;
    public static float dashDeceleration = 0.5f;

    public static float jumpSpeed = 2f;
    public static int jumpHoldTime = 250;
    public static int jumpBufferTime = 50;

    public static float gravity = 0.5f;
    public static float maxFallSpeed = 2;

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 0.09f;
        horizontalDeceleration = 0.08f;
        maxSpeed = 1.2f;

        dashSpeed = 3;
        dashTime = 150;
        dashDeceleration = 0.5f;
        
        jumpSpeed = 2;
        jumpHoldTime = 250;
        jumpBufferTime = 50;

        gravity = 0.5f;
        maxFallSpeed = 2;
    }
}