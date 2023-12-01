public class EnumHelper
{
    public static LevelObject GetObject(string INPUT) => INPUT switch
    {
        "PB" => LevelObject.PLATFORM_BOTTOM,
        "PBL" => LevelObject.PLATFORM_BOTTOM_LEFT,
        "PBR" => LevelObject.PLATFORM_BOTTOM_RIGHT,
        "PH" => LevelObject.PLATFORM_HORIZONTAL,
        "PL" => LevelObject.PLATFORM_LEFT,
        "PO" => LevelObject.PLATFORM_OPEN,
        "POB" => LevelObject.PLATFORM_OPEN_BOTTOM,
        "POL" => LevelObject.PLATFORM_OPEN_LEFT,
        "POR" => LevelObject.PLATFORM_OPEN_RIGHT,
        "POT" => LevelObject.PLATFORM_OPEN_TOP,
        "PR" => LevelObject.PLATFORM_RIGHT,
        "PS" => LevelObject.PLATFORM_SINGLE,
        "PT" => LevelObject.PLATFORM_TOP,
        "PTL" => LevelObject.PLATFORM_TOP_LEFT,
        "PTR" => LevelObject.PLATFORM_TOP_RIGHT,
        "PV" => LevelObject.PLATFORM_VERTICAL,
        "H" => LevelObject.HAZARD,
        "PC" => LevelObject.PLAYER,
        "S" => LevelObject.START,
        "O" => LevelObject.OBJECTIVE,
        _ => LevelObject.EMPTY
    };

    public static string GetPlatformPath(LevelObject obj) => "Platforms//" + obj switch
        {
            LevelObject.PLATFORM_BOTTOM => "platformBottom",
            LevelObject.PLATFORM_BOTTOM_LEFT => "platformBottomLeft",
            LevelObject.PLATFORM_BOTTOM_RIGHT => "platformBottomRight",
            LevelObject.PLATFORM_HORIZONTAL => "platformHorizontal",
            LevelObject.PLATFORM_LEFT => "platformLeft",
            LevelObject.PLATFORM_OPEN => "platformOpen",
            LevelObject.PLATFORM_OPEN_BOTTOM => "platformOpenBottom",
            LevelObject.PLATFORM_OPEN_LEFT => "platformOpenLeft",
            LevelObject.PLATFORM_OPEN_RIGHT => "platformOpenRight",
            LevelObject.PLATFORM_OPEN_TOP => "platformOpenTop",
            LevelObject.PLATFORM_RIGHT => "platformRight",
            LevelObject.PLATFORM_SINGLE => "platformSingle",
            LevelObject.PLATFORM_TOP => "platformTop",
            LevelObject.PLATFORM_TOP_LEFT => "platformTopLeft",
            LevelObject.PLATFORM_TOP_RIGHT => "platformTopRight",
            LevelObject.PLATFORM_VERTICAL => "platformVertical",
            _ => "platformBunting"
        };
}