using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class MainMenu
{
    public Sprite bkg;
    public TextComponent versionNo;

    public List<Button> buttons = new();

    public MainMenu() {

        bkg = new SpriteBuilder().WithPath("DraftPokerLogo").WithOffset(new Vector2(0,0)).WithDims(new Vector2(900,900)).Build();

        versionNo = new TextComponentBuilder().WithText("Version 0.1")
                                            .WithScreenAlignment(Alignment.BOTTOM_RIGHT)
                                            .WithTextAlignment(Alignment.CENTER_RIGHT)
                                            .WithOffset(new Vector2(-30,-30))
                                            .WithColor(Colors.ShamrockGreen)
                                            .Build();

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button96x32")
                                                        .WithColor(Colors.Buttons)
                                                        .WithDims(new Vector2(110,32))
                                                        .WithScreenAlignment(Alignment.BOTTOM_LEFT);

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-150)).WithText("Exit").WithButtonAction((sender, info) => { Environment.Exit(0); }).BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-390))
                                .WithText("Play")
                                .WithButtonAction(TransitionManager.ChangeGameState)
                                .WithButtonInfo(GameState.GAME_PLAY)
                                .BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-310)).WithText("Options").WithButtonInfo(GameState.OPTIONS).BuildButton());
        
        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-230)).WithText("About").WithButtonInfo(GameState.ABOUT).BuildButton());
    }

    public virtual void Update()
    {
        bkg.Update();
        versionNo.Update();
        foreach (Button button in buttons)
        {
            button.Update();
        }
    }

    public virtual void Draw()
    {
        bkg.Draw();
        versionNo.Draw();

        foreach (Button button in buttons)
        {
            button.Draw();
        }
    }
}
