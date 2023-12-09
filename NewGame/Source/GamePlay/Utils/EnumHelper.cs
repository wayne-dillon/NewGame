public class EnumHelper
{
    public static LevelObject GetObject(string INPUT) => INPUT switch
    {
        "PB-" => LevelObject.PLATFORM_BOTTOM,
        "PBL" => LevelObject.PLATFORM_BOTTOM_LEFT,
        "PBR" => LevelObject.PLATFORM_BOTTOM_RIGHT,
        "PH-" => LevelObject.PLATFORM_HORIZONTAL,
        "PL-" => LevelObject.PLATFORM_LEFT,
        "PO-" => LevelObject.PLATFORM_OPEN,
        "POB" => LevelObject.PLATFORM_OPEN_BOTTOM,
        "POL" => LevelObject.PLATFORM_OPEN_LEFT,
        "POR" => LevelObject.PLATFORM_OPEN_RIGHT,
        "POT" => LevelObject.PLATFORM_OPEN_TOP,
        "PR-" => LevelObject.PLATFORM_RIGHT,
        "PS-" => LevelObject.PLATFORM_SINGLE,
        "PT-" => LevelObject.PLATFORM_TOP,
        "PTL" => LevelObject.PLATFORM_TOP_LEFT,
        "PTR" => LevelObject.PLATFORM_TOP_RIGHT,
        "PV-" => LevelObject.PLATFORM_VERTICAL,
        "-H-" => LevelObject.HAZARD,
        "PC-" => LevelObject.PLAYER,
        "-S-" => LevelObject.START,
        "-O-" => LevelObject.OBJECTIVE,
        "-T-" => LevelObject.TEXT,
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

    public static string GetLevelPath(LevelSelection selection) => selection switch
    {
        LevelSelection.TUTORIAL => "Source//Content//Levels//Tutorial.csv",
        LevelSelection.LEVEL_1 => "Source//Content//Levels//Level1.csv",
        LevelSelection.LEVEL_2 => "Source//Content//Levels//Level2.csv",
        LevelSelection.LEVEL_3 => "Source//Content//Levels//Level3.csv",
        _ => "Source//Content//Levels//Level3.csv",
    };

    public static string GetLevelBackdropPath(LevelSelection selection) => selection switch
    {
        LevelSelection.TUTORIAL => "Background//clouds",
        LevelSelection.LEVEL_1 => "Background//rain",
        LevelSelection.LEVEL_2 => "Background//storm",
        LevelSelection.LEVEL_3 => "Background//night",
        _ => "Background//night",
    };

    public static string GetLevelText(int NUM) => NUM switch
    {
        0 => "Use Left and Right keys\nor 'A' and 'D' to move",
        1 => "Press Up or 'W' to Jump",
        2 => "In Cat mode, press\nSpace to dash",
        3 => "Left mouse click or\npress 'Q' to switch\nto Frog mode",
        4 => "Frog mode allows you\nto perform a double jump",
        5 => "Left click or 'Q' again\nto switch to Monkey mode",
        6 => "In Monkey mode you can\nclimb up and jump off walls",
        7 => "Right mouse and 'E'\ncycle through modes in\nin the opposite direction",
        8 => "The countdown begins when\nyou pass the start line",
        9 => "Turn all phones to airplane\nmode to finish the level",
        _ => ""
    };
}