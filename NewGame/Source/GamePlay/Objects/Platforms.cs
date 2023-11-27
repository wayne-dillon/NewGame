using System.Collections.Generic;

public class Platforms
{
    public static List<Hitbox> hitboxes = new();

    public void Reset()
    {
        hitboxes.Clear();
    }
}