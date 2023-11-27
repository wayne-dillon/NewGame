public class InputController
{
    public static bool Left() => Globals.keyboard.GetPress("A") || Globals.keyboard.GetPress("Left");

    public static bool Right() => Globals.keyboard.GetPress("D") || Globals.keyboard.GetPress("Right");

    public static bool Jump() => Globals.keyboard.GetPress("W") || Globals.keyboard.GetPress("Up") || Globals.keyboard.GetPress(" ");
}