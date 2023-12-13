using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class LevelEditor
{
    private List<string[]> array;
    private List<EditTile> tiles;
    private int moveSpeed = 5;

    public LevelEditor()
    {
        Init(null, null);
    }

    public void Init(object SENDER, object INFO)
    {
        tiles = new();
        Globals.screenPosition = Vector2.Zero;
        array = CSVReader.ReadFile(EnumHelper.GetLevelPath(GameGlobals.currentLevel));
        Vector2 pos = Vector2.Zero;
        bool firstLine = true;
        foreach (string[] line in array)
        {
            if (firstLine)
            {
                firstLine = false;
            } else{
                pos.Y++;
            }
            pos.X = 0;
            bool firstColumn = true;
            foreach (string obj in line)
            {
                if (firstColumn)
                {
                    firstColumn = false;
                } else{
                    pos.X++;
                }
                tiles.Add(new(obj, pos, UpdateArray));
            }
        }
    }

    public void Update()
    {
        foreach (EditTile tile in tiles)
        {
            tile.Update();
        }

        if (InputController.Up())
        {
            Globals.screenPosition += new Vector2(0, -moveSpeed);
        }
        if (InputController.Down())
        {
            Globals.screenPosition += new Vector2(0, moveSpeed);
        }
        if (InputController.Left())
        {
            Globals.screenPosition += new Vector2(-moveSpeed, 0);
        }
        if (InputController.Right())
        {
            Globals.screenPosition += new Vector2(moveSpeed, 0);
        }
    }

    public void UpdateArray(object SENDER, object INFO)
    {
        if (INFO is EditTile tile)
        {
            array[(int)tile.coords.Y][(int)tile.coords.X] = EnumHelper.GetObjectString(tile.currentType);
        }
        CSVWriter.WriteFile(EnumHelper.GetLevelPath(GameGlobals.currentLevel), array);
    }

    public void Draw()
    {
        foreach (EditTile tile in tiles)
        {
            tile.Draw();
        }
    }
}