using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class LevelEditor
{
    private List<string[]> array;
    private List<EditTile> tiles = new();

    public LevelEditor()
    {
        Init(null, null);
    }

    public void Init(object SENDER, object INFO)
    {
        array = CSVReader.ReadFile(EnumHelper.GetLevelPath(GameGlobals.currentLevel));
        Vector2 pos = Vector2.Zero;
        foreach (string[] line in array)
        {
            pos.X = 0;
            foreach (string obj in line)
            {
                tiles.Add(new(obj, pos, UpdateArray));
                pos.X++;
            }
            pos.Y++;
        }
    }

    public void Update()
    {
        foreach (EditTile tile in tiles)
        {
            tile.Update();
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