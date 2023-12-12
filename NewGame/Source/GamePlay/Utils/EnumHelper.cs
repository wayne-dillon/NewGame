using Microsoft.Xna.Framework.Graphics;

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
    
    public static string GetObjectString(LevelObject INPUT) => INPUT switch
    {
        LevelObject.PLATFORM_BOTTOM => "PB-",
        LevelObject.PLATFORM_BOTTOM_LEFT => "PBL",
        LevelObject.PLATFORM_BOTTOM_RIGHT => "PBR",
        LevelObject.PLATFORM_HORIZONTAL => "PH-",
        LevelObject.PLATFORM_LEFT => "PL-",
        LevelObject.PLATFORM_OPEN => "PO-",
        LevelObject.PLATFORM_OPEN_BOTTOM => "POB",
        LevelObject.PLATFORM_OPEN_LEFT => "POL",
        LevelObject.PLATFORM_OPEN_RIGHT => "POR",
        LevelObject.PLATFORM_OPEN_TOP => "POT",
        LevelObject.PLATFORM_RIGHT => "PR-",
        LevelObject.PLATFORM_SINGLE => "PS-",
        LevelObject.PLATFORM_TOP => "PT-",
        LevelObject.PLATFORM_TOP_LEFT => "PTL",
        LevelObject.PLATFORM_TOP_RIGHT => "PTR",
        LevelObject.PLATFORM_VERTICAL => "PV-",
        LevelObject.HAZARD => "-H-",
        LevelObject.PLAYER => "PC-",
        LevelObject.START => "-S-",
        LevelObject.OBJECTIVE => "-O-",
        LevelObject.TEXT => "-T-",
        _ => "---"
    };

    public static string GetObjectPath(LevelObject obj) => obj switch
    {
        LevelObject.PLATFORM_BOTTOM => "Platforms//platformBottom",
        LevelObject.PLATFORM_BOTTOM_LEFT => "Platforms//platformBottomLeft",
        LevelObject.PLATFORM_BOTTOM_RIGHT => "Platforms//platformBottomRight",
        LevelObject.PLATFORM_HORIZONTAL => "Platforms//platformHorizontal",
        LevelObject.PLATFORM_LEFT => "Platforms//platformLeft",
        LevelObject.PLATFORM_OPEN => "Platforms//platformOpen",
        LevelObject.PLATFORM_OPEN_BOTTOM => "Platforms//platformOpenBottom",
        LevelObject.PLATFORM_OPEN_LEFT => "Platforms//platformOpenLeft",
        LevelObject.PLATFORM_OPEN_RIGHT => "Platforms//platformOpenRight",
        LevelObject.PLATFORM_OPEN_TOP => "Platforms//platformOpenTop",
        LevelObject.PLATFORM_RIGHT => "Platforms//platformRight",
        LevelObject.PLATFORM_SINGLE => "Platforms//platformSingle",
        LevelObject.PLATFORM_TOP => "Platforms//platformTop",
        LevelObject.PLATFORM_TOP_LEFT => "Platforms//platformTopLeft",
        LevelObject.PLATFORM_TOP_RIGHT => "Platforms//platformTopRight",
        LevelObject.PLATFORM_VERTICAL => "Platforms//platformVertical",
        LevelObject.EMPTY => "UI//Oval20x20",
        LevelObject.HAZARD => "Symbols//Diamonds",
        LevelObject.OBJECTIVE => "Symbols//ringingPhone1",
        LevelObject.PLAYER => "Player//Cat//Idle1",
        LevelObject.START => "Symbols//Checker",
        _ => "Platforms//platformBunting"
    };

    public static Texture2D GetObjectTexture(LevelObject obj)
    {
        return Globals.content.Load<Texture2D>(GetObjectPath(obj));
    }

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