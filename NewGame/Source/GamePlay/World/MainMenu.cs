using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class MainMenu
{
    public Sprite bkg;
    public TextComponent versionNo;
    private bool levelSelect = false;

    public List<Button> buttons = new();
    public List<Button> levels = new();
    public Button levelSelectButton;

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

        levelSelectButton = buttonBuilder.WithText("Levels")
                                        .WithButtonAction((sender, info) => { levelSelect = true; })
                                        .WithOffset(new Vector2(180, -470))
                                        .BuildButton();

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-150)).WithText("Exit").WithButtonAction((sender, info) => { Environment.Exit(0); }).BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-390))
                                .WithText("Play")
                                .WithButtonAction(Continue)
                                .BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-310)).WithText("Options").WithButtonInfo(GameState.OPTIONS).BuildButton());
        
        buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-230)).WithText("About").WithButtonInfo(GameState.ABOUT).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(350, -470)).WithText("Tutorial").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.TUTORIAL).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(350, -390)).WithText("Level 1").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_1).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(350, -310)).WithText("Level 2").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_2).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(350, -230)).WithText("Level 3").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_3).BuildButton());
    }

    public virtual void Update()
    {
        bkg.Update();
        versionNo.Update();
        foreach (Button button in buttons)
        {
            button.Update();
        }
        if (Persistence.preferences.levelsComplete > 0)
        {
            levelSelectButton.Update();
        }
        if (levelSelect)
        {
            for (int i = 0; i <= Persistence.preferences.levelsComplete; i++)
            {
                levels[i].Update();
            }
        }
    }

    private void Continue(object SENDER, object INFO)
    {
        GameGlobals.currentLevel = Persistence.preferences.levelsComplete > 3 ? 
                LevelSelection.LEVEL_3 : (LevelSelection)Persistence.preferences.levelsComplete;
        TransitionManager.ChangeGameState(null, GameState.GAME_PLAY);
    }

    private void SelectLevel(object SENDER, object INFO)
    {
        if (INFO is LevelSelection level)
        {
            GameGlobals.currentLevel = level;
            TransitionManager.ChangeGameState(null, GameState.GAME_PLAY);
            levelSelect = false;
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
        if (Persistence.preferences.levelsComplete > 0)
        {
            levelSelectButton.Draw();
        }
        if (levelSelect)
        {
            for (int i = 0; i <= Persistence.preferences.levelsComplete; i++)
            {
                levels[i].Draw();
            }
        }
    }
}
