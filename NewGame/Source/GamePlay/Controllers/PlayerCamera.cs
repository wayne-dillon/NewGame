using Microsoft.Xna.Framework;

public class PlayerCamera
{
    private Sprite playerSprite;
    private Level level;
    private Vector2 offset = new(860, 540);

    public PlayerCamera(Sprite PLAYER, Level LEVEL)
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
        if (playerCamX > level.right - Coordinates.screenWidth)
        {
            return level.right - Coordinates.screenWidth;
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
        if (playerCamY > level.bottom - Coordinates.screenHeight)
        {
            return level.bottom - Coordinates.screenHeight;
        }
        return playerCamY;
    }
}