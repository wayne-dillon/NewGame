using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class DevConsole
{
    private List<ValueSelector> valueSelectors = new();
    private Button resetButton;

    public DevConsole()
    {
        resetButton = new SpriteBuilder().WithPath("UI//Button96x32")
                                        .WithDims(new Vector2(96,32))
                                        .WithText("Reset")
                                        .WithScreenAlignment(Alignment.TOP)
                                        .WithOffset(new Vector2(0, 100))
                                        .WithButtonAction(PlayerMovementValues.ResetValues)
                                        .BuildButton();

        int height = -360;
        for (int i = 0; i < 10 ; i++)
        {
            valueSelectors.Add(new((ValueSelector.Variable)i, new Vector2(0, height)));
            height += 80;
        }
    }

    public void Update()
    {
        resetButton.Update();
        foreach (ValueSelector selector in valueSelectors)
        {
            selector.Update();
        }
    }

    public void Draw()
    {
        resetButton.Draw();
        foreach (ValueSelector selector in valueSelectors)
        {
            selector.Draw();
        }
    }
}