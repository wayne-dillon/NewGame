using Microsoft.Xna.Framework;

public class Camera
{
    private Sprite playerSprite;
    private Vector2 offset = new(860, 800);

    public Camera(Sprite PLAYER)
    {
        playerSprite = PLAYER;
    }

    public void Update()
    {
        Globals.screenPosition = playerSprite.Pos - offset;
    }
}