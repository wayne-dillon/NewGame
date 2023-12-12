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
                                .WithButtonAction(Play)
                                .BuildButton());

        buttons.Add(buttonBuilder.WithOffset(new Vector2(-257,250))
                                .WithPath("UI//menuButton2")
                                .WithDims(new Vector2(514,119))
                                .WithText("Options")
                                .WithButtonAction(TransitionManager.ChangeGameState)
                                .WithButtonInfo(GameState.ABOUT)
                                .BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(-257,-50)).WithText("Tutorial").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.TUTORIAL).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(-257,80)).WithText("Level 1").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_1).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(-257,210)).WithText("Level 2").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_2).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(-257,340)).WithText("Level 3").WithButtonAction(SelectLevel).WithButtonInfo(LevelSelection.LEVEL_3).BuildButton());

        levels.Add(buttonBuilder.WithOffset(new Vector2(-257,470)).WithText("Back").WithButtonAction((sender, info) => { levelSelect = false; }).WithButtonInfo(null).BuildButton());
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

        if (levelSelect)
        {
            for (int i = 0; i <= Persistence.preferences.levelsComplete; i++)
            {
                levels[i].Update();
            }
            levels[4].Update();
        } else {
            foreach (Button button in buttons)
            {
                button.Update();
            }
        }
    }

    private void Play(object SENDER, object INFO)
    {
        if (Persistence.preferences.levelsComplete == 0)
        {
            GameGlobals.currentLevel = LevelSelection.TUTORIAL;
            TransitionManager.ChangeGameState(null, GameState.GAME_PLAY);
            return;
        }

        levelSelect = true;
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

        if (levelSelect)
        {
            for (int i = 0; i <= Persistence.preferences.levelsComplete; i++)
            {
                levels[i].Draw();
            }
            levels[4].Draw();
        } else {
            foreach (Button button in buttons)
            {
                button.Draw();
            }
        }
    }
}
