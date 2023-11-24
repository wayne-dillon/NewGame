using Microsoft.Xna.Framework.Graphics;

public class Fonts
{
    public static SpriteFont defaultFont;
    public static SpriteFont titleFont;
    public static SpriteFont numberFont;

    public static void Init()
    {
        defaultFont = Globals.content.Load<SpriteFont>("Fonts//Ubuntu");
        titleFont = Globals.content.Load<SpriteFont>("Fonts//UbuntuBold");
        numberFont = Globals.content.Load<SpriteFont>("Fonts//PressStart");
    }
}