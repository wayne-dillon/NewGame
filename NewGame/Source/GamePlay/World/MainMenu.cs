using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class MainMenu
{
    public Sprite bkg;
    public List<Sprite> clouds = new();
    public Sprite logo;
    public Sprite title;
    public TextComponent versionNo;
    private bool levelSelect = false;

    public List<Button> buttons = new();
    public List<Button> levels = new();
    public Button levelSelectButton;

    public MainMenu() {
        for (int i = 0; i < 4; i++)
        {
            clouds.Add(new SpriteBuilder().WithPath("Background//clouds1")
                                        .WithDims(new Vector2(Coordinates.screenWidth,Coordinates.screenHeight))
                                        .WithOffset(new Vector2(i * Coordinates.screenWidth, 0))
                                        .WithAnimation(new ScrollHorizontal(-0.2f))
                                        .Build());
        }

        bkg = new SpriteBuilder().WithPath("Background//menuLogo").WithDims(new Vector2(Coordinates.screenWidth,Coordinates.screenHeight)).Build();
        logo = new SpriteBuilder().WithPath("Background//logo").WithDims(new Vector2(Coordinates.screenWidth,Coordinates.screenHeight)).Build();
        title = new SpriteBuilder().WithPath("Background//title").WithDims(new Vector2(Coordinates.screenWidth,Coordinates.screenHeight)).Build();

        versionNo = new TextComponentBuilder().WithText("Version 0.1")
                                            .WithScreenAlignment(Alignment.BOTTOM_RIGHT)
                                            .WithTextAlignment(Alignment.CENTER_RIGHT)
                                            .WithOffset(new Vector2(-30,-30))
                                            .WithColor(Colors.ShamrockGreen)
                                            .Build();

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithScreenAlignment(Alignment.CENTER_RIGHT);

        levelSelectButton = buttonBuilder
                                .WithPath("UI//menuButton3").WithText("Levels")
                                        .WithButtonAction((sender, info) => { levelSelect = true; })
                                        .WithOffset(new Vector2(180, -470))
                                        .BuildButton();

        buttons.Add(buttonBuilder.WithOffset(new Vector2(-189,400))
                                .WithPath("UI//menuButton3")
                                .WithDims(new Vector2(398,86))
                                .WithText("Quit")
                                .WithButtonAction((sender, info) => { Environment.Exit(0); })
                                .BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(-312,100))
                                .WithPath("UI//menuButton1")
                                .WithDims(new Vector2(624,128))
                                .WithText("Play")
                                .WithButtonAction(Continue)
                                .BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(-257,250))
                                .WithPath("UI//menuButton2")
                                .WithDims(new Vector2(514,119))
                                .WithText("Options")
                                .WithButtonAction(TransitionManager.ChangeGameState)
                                .WithButtonInfo(GameState.OPTIONS)
                                .BuildButton());
        
        // buttons.Add(buttonBuilder.WithOffset(new Vector2(180,-230)).WithText("About").WithButtonInfo(GameState.ABOUT).BuildButton());

        // levels.Add(buttonBuilder.WithOffset(new Vector2(350, -470)).WithText("Tutorial").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.TUTORIAL).BuildButton());

        // levels.Add(buttonBuilder.WithOffset(new Vector2(350, -390)).WithText("Level 1").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_1).BuildButton());

        // levels.Add(buttonBuilder.WithOffset(new Vector2(350, -310)).WithText("Level 2").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_2).BuildButton());

        // levels.Add(buttonBuilder.WithOffset(new Vector2(350, -230)).WithText("Level 3").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_3).BuildButton());
    }

    public virtual void Update()
    {
        foreach (Sprite cloud in clouds)
        {
            cloud.Update();
        }
        bkg.Update();
        logo.Update();
        title.Update();
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
        foreach (Sprite cloud in clouds)
        {
            cloud.Draw();
        }
        bkg.Draw();
        logo.Draw();
        title.Draw();
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
