using Microsoft.Xna.Framework.Graphics;

public class Fonts
{
    public static SpriteFont defaultFont12;
    public static SpriteFont defaultFont18;
    public static SpriteFont defaultFont24;
    public static SpriteFont numberFont;

    public static void Init()
    {
        defaultFont12 = Globals.content.Load<SpriteFont>("Fonts//IndieFlower12");
        defaultFont18 = Globals.content.Load<SpriteFont>("Fonts//IndieFlower18");
        defaultFont24 = Globals.content.Load<SpriteFont>("Fonts//IndieFlower24");
        numberFont = Globals.content.Load<SpriteFont>("Fonts//PressStart");
    }
}