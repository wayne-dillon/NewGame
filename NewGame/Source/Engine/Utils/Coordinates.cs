using Microsoft.Xna.Framework;

public class Coordinates
{
    public static int screenHeight, screenWidth;

    public static Vector2 Origin() => new(screenWidth/2, screenHeight/2);

    public static Vector2 CenterLeft() => new(0, screenHeight/2);

    public static Vector2 CenterRight() => new(screenWidth, screenHeight/2);

    public static Vector2 TopCenter() => new(screenWidth/2, 0);

    public static Vector2 TopLeft() => new(0, 0);

    public static Vector2 TopRight() => new(screenWidth, 0);

    public static Vector2 BottomCenter() => new(screenWidth/2, screenHeight);

    public static Vector2 BottomLeft() => new(0, screenHeight);

    public static Vector2 BottomRight() => new(screenWidth, screenHeight);
 
    public static Vector2 Get(Alignment alignment) => alignment switch
        {
            Alignment.CENTER_LEFT => CenterLeft(),
            Alignment.CENTER_RIGHT => CenterRight(),
            Alignment.TOP => TopCenter(),
            Alignment.TOP_LEFT => TopLeft(),
            Alignment.TOP_RIGHT => TopRight(),
            Alignment.BOTTOM => BottomCenter(),
            Alignment.BOTTOM_LEFT => BottomLeft(),
            Alignment.BOTTOM_RIGHT => BottomRight(),
            _ => Origin(),
        };
}