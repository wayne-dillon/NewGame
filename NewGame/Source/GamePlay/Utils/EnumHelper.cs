public class EnumHelper
{
    public static LevelObject GetObject(string INPUT) => INPUT switch
    {
        "P" => LevelObject.PLATFORM,
        "H" => LevelObject.HAZARD,
        "PC" => LevelObject.PLAYER,
        "S" => LevelObject.START,
        "O" => LevelObject.OBJECTIVE,
        _ => LevelObject.EMPTY
    };
}