public struct PlayerMovementValues
{
    public static int horizontalAcceleration;
    public static int horizontalDeceleration;
    public static int maxSpeed;
    public static float HorizontalAcceleration{ get { return (float)horizontalAcceleration/1000; } }
    public static float HorizontalDeceleration{ get { return (float)horizontalDeceleration/1000; } }
    public static float MaxSpeed{ get { return (float)maxSpeed/1000; } }

    public static float climbSpeedRatio = 2f/3f;

    public static int dashSpeed;
    public static int dashDeceleration;
    public static float DashSpeed{ get { return (float)dashSpeed/1000; } }
    public static int dashTime;
    public static float DashDeceleration{ get { return (float)dashDeceleration/1000; } }

    public static int jumpSpeed;
    public static float JumpSpeed{ get { return (float)jumpSpeed/1000; } }
    public static int jumpHoldTime;
    public static int jumpBufferTime = 50;

    public static int gravity;
    public static int maxFallSpeed;
    public static float Gravity{ get { return (float)gravity/1000; } }
    public static float MaxFallSpeed{ get { return (float)maxFallSpeed/1000; } }

    public static void ResetValues(object SENDER, object INFO)
    {
        horizontalAcceleration = 100;
        horizontalDeceleration = 150;
        maxSpeed = 600;

        dashSpeed = 2000;
        dashTime = 220;
        dashDeceleration = 500;
        
        jumpSpeed = 900;
        jumpHoldTime = 250;

        gravity = 60;
        maxFallSpeed = 900;
    }
}