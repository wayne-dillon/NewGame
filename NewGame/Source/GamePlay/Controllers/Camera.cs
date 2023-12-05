using Microsoft.Xna.Framework;

public class Camera
{
    private Sprite playerSprite;
    private Level level;
    private Vector2 offset = new(860, 800);

    public Camera(Sprite PLAYER, Level LEVEL)
    {
        playerSprite = PLAYER;
        level = LEVEL;
    }

    public void Update()
    {
        Globals.screenPosition.X = GetX();
        Globals.screenPosition.Y = GetY();
    }

    private float GetX()
    {
        float playerCamX = playerSprite.Pos.X - offset.X;
        if (playerCamX < level.left)
        {
            return level.left;
        }
        if (playerCamX > level.right - Globals.screenWidth)
        {
            return level.right - Globals.screenWidth;
        }
        return playerCamX;
    }

    private float GetY()
    {
        float playerCamY = playerSprite.Pos.Y - offset.Y;
        if (playerCamY < level.top)
        {
            return level.top;
        }
        if (playerCamY > level.bottom - Globals.screenHeight)
        {
            return level.bottom - Globals.screenHeight;
        }
        return playerCamY;
    }
}