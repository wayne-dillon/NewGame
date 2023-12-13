using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class DevConsole
{
    private List<ValueSelector> valueSelectors = new();
    private Button resetButton;

    public DevConsole()
    {
        resetButton = new SpriteBuilder().WithPath("UI//Button397x114")
                                        .WithDims(new Vector2(120,30))
                                        .WithText("Reset")
                                        .WithScreenAlignment(Alignment.BOTTOM)
                                        .WithOffset(new Vector2(0, -120))
                                        .WithButtonAction(PlayerMovementValues.ResetValues)
                                        .BuildButton();

        int height = -320;
        for (int i = 0; i <= 10 ; i++)
        {
            valueSelectors.Add(new((ValueSelector.Variable)i, new Vector2(0, height)));
            height += 60;
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