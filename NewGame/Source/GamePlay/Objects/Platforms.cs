using System.Collections.Generic;

public class Platforms
{
    public static List<Hitbox> hitboxes = new();

    public static void Reset()
    {
        hitboxes.Clear();
    }
}