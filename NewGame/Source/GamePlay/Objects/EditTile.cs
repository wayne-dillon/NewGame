using System;
using Microsoft.Xna.Framework;

public class EditTile
{
    public Vector2 coords;
    public LevelObject currentType;
    private Clickable clickable;
    private readonly int tileSize = 60;
    private EventHandler<object> action;

    public EditTile(string TYPE, Vector2 COORDS, EventHandler<object> ACTION)
    {
        action = ACTION;
        coords = COORDS;
        currentType = EnumHelper.GetObject(TYPE);
        clickable = new SpriteBuilder().WithPath(EnumHelper.GetObjectPath(currentType))
                                    .WithAbsolutePosition(COORDS * tileSize)
                                    .WithDims(new Vector2(tileSize, tileSize))
                                    .WithButtonAction(ChangeType)
                                    .BuildClickable();
    }

    public void Update()
    {
        clickable.Update();
    }

    public void ChangeType(object SENDER, object INFO)
    {
        switch (currentType)
        {
            case LevelObject.EMPTY:
                currentType = LevelObject.HAZARD;
                break;
            case LevelObject.HAZARD:
                currentType = LevelObject.OBJECTIVE;
                break;
            case LevelObject.OBJECTIVE:
                currentType = LevelObject.PLATFORM_SINGLE;
                break;
            case LevelObject.PLATFORM_SINGLE:
                currentType = LevelObject.PLAYER;
                break;
            case LevelObject.PLAYER:
                currentType = LevelObject.START;
                break;
            case LevelObject.START:
            default:
                currentType = LevelObject.EMPTY;
                break;
        }
        clickable.myModel = EnumHelper.GetObjectTexture(currentType);
        action(null, this);
    }

    public void Draw()
    {
        clickable.Draw();
    }
}