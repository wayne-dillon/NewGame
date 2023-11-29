using System.Collections.Generic;

public class Hazards
{
    public static List<Hitbox> hitboxes = new();

    public static void Reset()
    {
        hitboxes.Clear();
    }
}