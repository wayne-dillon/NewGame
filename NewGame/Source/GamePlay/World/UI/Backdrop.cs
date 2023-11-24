using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class Backdrop
{
    public readonly List<Sprite> symbols = new();
    public IAnimate animation = new ScrollDiagonal(0.03f);

    public Backdrop()
    {
        int suit = 0;
        for (int i = -10; i < 30; i++)
        {
            for (int j = -1; j < 10; j++)
            {
                symbols.Add(new SpriteBuilder().WithDims(new Vector2(29, 29))
                                        .WithPath("Symbols//Spades") 
                                        .WithAbsolutePosition(new Vector2(i*100, j*100))
                                        .WithTransitionable(false)
                                        .Build());
                suit++;
            }
        }
    }

    public void Update()
    {
        foreach (Sprite symbol in symbols)
        {
            animation.Animate(symbol);
        }
    }

    public void Draw()
    {
        foreach (Sprite symbol in symbols)
        {
            if (symbol.IsOnScreen())
            {
                symbol.Draw();
            }
        }
    }
}